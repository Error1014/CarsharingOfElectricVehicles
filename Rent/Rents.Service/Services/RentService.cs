using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.DTO.Rent;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
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
        private readonly UriEndPoint _getBalanceUri;
        private readonly UriEndPoint _updateBalanceUri;
        private readonly UriEndPoint _boockingCarUri;
        private readonly UriEndPoint _cancelBoockingCarUri;
        private readonly UriEndPoint _mySubscription;
        public RentService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;

            _getBalanceUri = configuration.GetSection("EndPoint:GetBalance").Get<UriEndPoint>();
            _updateBalanceUri = configuration.GetSection("EndPoint:UpdateBalance").Get<UriEndPoint>();
            _boockingCarUri = configuration.GetSection("EndPoint:BoockingCar").Get<UriEndPoint>();
            _cancelBoockingCarUri = configuration.GetSection("EndPoint:CancelBoockingCar").Get<UriEndPoint>();
            _mySubscription = configuration.GetSection("EndPoint:GetMySubscription").Get<UriEndPoint>();

        }

        private async Task<decimal?> GetBalance()
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_getBalanceUri.BaseAddress);
            var response = await _httpClient.GetAsync(_getBalanceUri.Uri + _userSessionGetter.UserId);
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
            if (rentDTO.TariffId==null)
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
            _httpClient.BaseAddress = new Uri(_updateBalanceUri.BaseAddress);
            summ = 0 - summ;
            var transact = new TransactionItemDTO();
            transact.ClientId = _userSessionGetter.UserId;
            transact.Summ = summ;
            transact.DateTime = DateTime.Now;
            transact.TypeTransactionId = 3;
            var response = await _httpClient.PostAsync(_updateBalanceUri.Uri, JsonContent.Create(transact));
            response.EnsureSuccessStatusCode();
        }

        private async Task UpdateRentCar(Guid? carId, bool isRent)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_boockingCarUri.BaseAddress);
            HttpResponseMessage response = new HttpResponseMessage();
            if (isRent)
            {
                response = await _httpClient.PutAsync(_boockingCarUri.Uri + carId, JsonContent.Create(""));
            }
            else
            {
                response = await _httpClient.PutAsync(_cancelBoockingCarUri.Uri + carId, JsonContent.Create(""));
            }
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
        }

        private async Task<decimal> GetTotalPrice(Rent rent)
        {
            HttpClient _httpClient = new HttpClient();
            var tariff = await _unitOfWork.Tariffs.GetEntity(rent.TariffId.Value);
            if (tariff==null)
            {
                tariff = new Tariff();
                tariff.Price = 0;
                tariff.AdditionalPrice = 0;
            }
            var minutNoTariff = rent.DateTimeEndRent - rent.DateTimeBeginRent;
            int totalMin = minutNoTariff.Value.Minutes;

            _httpClient.BaseAddress = new Uri(_mySubscription.BaseAddress);
            var response = await _httpClient.GetAsync(_mySubscription.Uri+_userSessionGetter.UserId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var clientSubscription = JsonSerializer.Deserialize<SubscriptionDTO>(responseBody, options);
            var timeSubscription = clientSubscription.QuantityMinutsInDay; // нужно найти оставшиеся минуты
            var filter =new HistoryRentFilter();
            filter.DateTimeBeginRent = DateTime.Today;
            filter.DateTimeEndRent = DateTime.Now;
            var listRentToday = (await _unitOfWork.Rents.GetRentHistoryPage(filter)).ToList();
            int minutRentToday = 0;
            foreach (var item in listRentToday)
            {
                minutRentToday += (item.DateTimeEndRent - item.DateTimeBeginRent).Value.Minutes;
            }
            int min = 0;
            timeSubscription = timeSubscription - minutRentToday < 0 ? 0 : timeSubscription - minutRentToday;

            min = totalMin - timeSubscription;

            var tariffTime = tariff.Duration == null ? 0 : tariff.Duration.Value.Minutes;

            if (totalMin > tariffTime)
            {
                min = totalMin - tariffTime;
            }

            var result = min * tariff.AdditionalPrice + tariff.Price;
            return result;
        }
    }
}
