﻿using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.DTO.Rent;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
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

namespace Rents.Service.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        private HttpClient _httpClient;
        public RentService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;
            _httpClient = new HttpClient();
        }

        public async Task AddRent(AddRentDTO rentDTO)
        {
            var rent = _map.Map<Rent>(rentDTO);
            rent.ClientId = _userSessionGetter.UserId;
            rent.IsFinalSelectCar = false;
            rent.DateTimeBeginBoocking = DateTime.Now;
            await UpdateRentCar(rent.CarId, true);
            await _unitOfWork.Rents.AddEntities(rent);
            await _unitOfWork.Rents.SaveChanges();
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
        public async Task<IEnumerable<RentDTO>> GetRents(PageFilter pageFilter)
        {
            var rent = await _unitOfWork.Rents.GetPage(pageFilter);
            var result = _map.Map<IEnumerable<RentDTO>>(rent);
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
            rent.KilometersOutsideTariff = km;
            rent.TotalPrice = await GetTotalPrice(rent);
            _unitOfWork.Rents.UpdateEntities(rent);
            await _unitOfWork.Rents.SaveChanges();
        }

        private async Task<bool> UpdateRentCar(Guid? carId, bool isRent)
        {
            UriEndPoint uriEndPoint = new UriEndPoint(); //Заменить на взятие адреса из конфигурации
            _httpClient.BaseAddress = new Uri("https://localhost:7215");
            HttpResponseMessage response = new HttpResponseMessage();
            if (isRent)
            {
                response = await _httpClient.PutAsync("/api/Cars/BookingCar/" + carId, JsonContent.Create(""));
            }
            else
            {
                response = await _httpClient.PutAsync("/api/Cars/CancelBookingCar/" + carId, JsonContent.Create(""));
            }
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            isRent = JsonSerializer.Deserialize<bool>(responseBody);
            return isRent;
        }

        private async Task<decimal> GetTotalPrice(Rent rent)
        {
            var tariff = await _unitOfWork.Tariffs.GetEntity(rent.TariffId);
            var minutNoTariff = rent.DateTimeEndRent - rent.DateTimeBeginRent;
            int totalMin = minutNoTariff.Value.Minutes;

            _httpClient.BaseAddress = new Uri("https://localhost:7217");
            var response = await _httpClient.GetAsync("/api/Subscriptions");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var clientSubscription = JsonSerializer.Deserialize<SubscriptionDTO>(responseBody, options);
            var timeSubscription = clientSubscription.QuantityMinutsInDay; // нужно найти оставшиеся минуты

            int min = 0;
            if (totalMin > timeSubscription)
            {
                min = totalMin - timeSubscription;
            }

            var tariffTime = tariff.Duration.Value.Minutes;

            if (totalMin > tariffTime)
            {
                min = totalMin - tariffTime;
            }

            var result = min * tariff.AdditionalPrice + tariff.Price;
            return result;
        }
    }
}