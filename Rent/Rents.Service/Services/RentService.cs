﻿using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.DTO.Rent;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rents.Repository.Entities;
using Rents.Repository.Interfaces;
using Rents.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XAct.Events;

namespace Rents.Service.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        private readonly IConfiguration _configuration;
        public RentService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;
            _configuration = configuration;
        }

        private async Task<decimal?> GetBalance()
        {
            HttpClient _httpClient = new HttpClient();
            var getBalanceUri = _configuration.GetSection("EndPoint:GetBalance").Get<UriEndPoint>();
            _httpClient.BaseAddress = new Uri(getBalanceUri.BaseAddress);
            var response = await _httpClient.GetAsync(getBalanceUri.Uri + _userSessionGetter.UserId);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            decimal? balance = JsonSerializer.Deserialize<decimal?>(responseBody);
            return balance;
        }

        public async Task<Guid> AddRent(AddRentDTO rentDTO)
        {
            var balance = await GetBalance();
            if (balance < 0)
            {
                throw new BadRequestException("Недостаточно средств");
            }
            if (rentDTO.TariffId == null)
            {

            }
            else
            {
                var tariff = await _unitOfWork.Tariffs.GetEntity(rentDTO.TariffId.Value);
                if (tariff.Price > balance)
                {
                    throw new BadRequestException("Недостаточно средств");
                }
            }

            var rent = _map.Map<Rent>(rentDTO);
            rent.ClientId = _userSessionGetter.UserId;
            rent.IsFinalSelectCar = false;
            rent.DateTimeBeginBoocking = DateTime.Now;
            await UpdateRentCar(rent.CarId, true);
            await _unitOfWork.Rents.AddEntities(rent);
            await _unitOfWork.Rents.SaveChanges();
            return rent.Id;
        }
        public async Task CancelBookingCar()
        {
            var rent = await _unitOfWork.Rents.GetActualRent(_userSessionGetter.UserId);
            rent.IsFinalSelectCar = false;
            await UpdateRentCar(rent.CarId, false);
            _unitOfWork.Rents.UpdateEntities(rent);
            await _unitOfWork.Rents.SaveChanges();
        }
        public async Task<RentDTO> GetRent(Guid Id)
        {
            var rent = await _unitOfWork.Rents.GetEntity(Id);
            if (rent == null)
            {
                throw new NotFoundException("Запись не найдена");
            }
            var result = _map.Map<RentDTO>(rent);
            return result;
        }
        public async Task<RentDTO> GetActualRent()
        {
            var rent = await _unitOfWork.Rents.GetActualRent(_userSessionGetter.UserId);
            if (rent == null)
            {
                throw new NotFoundException("Запись не найдена");
            }
            var result = _map.Map<RentDTO>(rent);
            return result;
        }
        public async Task<Dictionary<Guid, RentDTO>> GetRents(HistoryRentFilter pageFilter)
        {
            if (pageFilter.ClientId == null)
            {
                if (_userSessionGetter.Role == "Client")
                {
                    pageFilter.ClientId = _userSessionGetter.UserId;
                }
            }
            var list = await _unitOfWork.Rents.GetPage(pageFilter);
            list = list.OrderBy(x => x.DateTimeBeginBoocking);
            Dictionary<Guid, RentDTO> result = new Dictionary<Guid, RentDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<RentDTO>(item));
            }
            return result;
        }

        public async Task StartTrip()
        {
            var rent = await _unitOfWork.Rents.GetActualRent(_userSessionGetter.UserId);
            rent.IsFinalSelectCar = true;
            rent.DateTimeBeginRent = DateTime.Now;
            _unitOfWork.Rents.UpdateEntities(rent);
            await _unitOfWork.Rents.SaveChanges();
        }

        public async Task EndTrip(decimal km)
        {
            var rent = await _unitOfWork.Rents.GetActualRent(_userSessionGetter.UserId);
            rent.DateTimeEndRent = DateTime.Now;
            rent.Kilometers = km;
            rent.TotalPrice = await GetTotalPrice(rent);
            _unitOfWork.Rents.UpdateEntities(rent);
            await UpdateRentCar(rent.CarId, false);
            await _unitOfWork.Rents.SaveChanges();
            await PayRent(rent.TotalPrice);
        }

        private async Task PayRent(decimal summ)
        {
            HttpClient _httpClient = new HttpClient();
            var updateBalanceUri = _configuration.GetSection("EndPoint:UpdateBalance").Get<UriEndPoint>();
            _httpClient.BaseAddress = new Uri(updateBalanceUri.BaseAddress);
            summ = 0 - summ;
            var transact = new TransactionItemDTO();
            transact.ClientId = _userSessionGetter.UserId;
            transact.Summ = summ;
            transact.DateTime = DateTime.Now;
            transact.TypeTransactionId = 3;
            var response = await _httpClient.PostAsync(updateBalanceUri.Uri, JsonContent.Create(transact));
            response.EnsureSuccessStatusCode();
        }

        private async Task UpdateRentCar(Guid? carId, bool isRent)
        {
            HttpClient _httpClient = new HttpClient();
            
            HttpResponseMessage response = new HttpResponseMessage();
            if (isRent)
            {
                var boockingCarUri = _configuration.GetSection("EndPoint:BoockingCar").Get<UriEndPoint>();
                _httpClient.BaseAddress = new Uri(boockingCarUri.BaseAddress);
                response = await _httpClient.PutAsync(boockingCarUri.Uri + carId, JsonContent.Create(""));
            }
            else
            {
                var cancelBoockingCarUri = _configuration.GetSection("EndPoint:CancelBoockingCar").Get<UriEndPoint>();
                _httpClient.BaseAddress = new Uri(cancelBoockingCarUri.BaseAddress);
                response = await _httpClient.PutAsync(cancelBoockingCarUri.Uri + carId, JsonContent.Create(""));
            }
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
        }
        private async Task<decimal> GetTotalPrice(Rent rent)
        {
            HttpClient _httpClient = new HttpClient();
            #region поиск подписки
            var mySubscription = _configuration.GetSection("EndPoint:GetMySubscription").Get<UriEndPoint>();
            _httpClient.BaseAddress = new Uri(mySubscription.BaseAddress);
            var response = await _httpClient.GetAsync(mySubscription.Uri + _userSessionGetter.UserId);
            
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var subscription = JsonSerializer.Deserialize<SubscriptionDTO>(responseBody, options);
            #endregion
            decimal totalPrice = 0;
            Guid minutTariffId = _configuration.GetSection("MinutTariff").Get<Guid>();
            bool isMinutTariff = minutTariffId == rent.TariffId ? true : false;
            int minutRent = (rent.DateTimeEndRent - rent.DateTimeBeginRent).Value.Minutes;

            var tariff = await _unitOfWork.Tariffs.GetEntity(rent.TariffId.Value);

            if (subscription.Name==null && isMinutTariff)
            {
                totalPrice = minutRent * tariff.PriceMinut;
            }
            else if (subscription.Name==null && !isMinutTariff)
            {
                var minutesOutsideTariff = minutRent - tariff.Duration > 0 ? tariff.Duration - minutRent : 0;
                totalPrice = tariff.Price + minutesOutsideTariff * tariff.PriceMinut;
            }
            else
            {
                var filter = new HistoryRentFilter();
                filter.DateTimeBeginRent = DateTime.Today;
                filter.DateTimeEndRent = DateTime.Now;
                var listRentToday = (await _unitOfWork.Rents.GetRentHistoryPage(filter)).ToList();
                int minutRentToday = 0;
                foreach (var item in listRentToday)
                {
                    minutRentToday += (item.DateTimeEndRent - item.DateTimeBeginRent).Value.Minutes;
                }
                var minutesOutsideSubscription = minutRentToday - subscription.QuantityMinutsInDay < 0 ? 0 : subscription.QuantityMinutsInDay - minutRentToday;
                var minutTariff = await _unitOfWork.Tariffs.GetEntity(minutTariffId);
                totalPrice = minutesOutsideSubscription * minutTariff.PriceMinut;
            }
            return totalPrice;
        }




    }
}
