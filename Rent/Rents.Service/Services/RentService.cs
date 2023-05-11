using AutoMapper;
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
    public class RentService: IRentService
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
            var rent = await _unitOfWork.Rents.GetActualBooking(_userSessionGetter.UserId);
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

        public async Task<IEnumerable<RentDTO>> GetRents(PageFilter pageFilter)
        {
            var rent = await _unitOfWork.Rents.GetPage(pageFilter);
            var result = _map.Map<IEnumerable<RentDTO>>(rent);
            return result;
        }

        private async Task<bool> UpdateRentCar(Guid? carId, bool isRent)
        {
            UriEndPoint uriEndPoint = new UriEndPoint(); //Заменить на взятие адреса из конфигурации
            _httpClient.BaseAddress = new Uri("https://localhost:7215");
            HttpResponseMessage response = new HttpResponseMessage();
            if (isRent)
            {
                response = await _httpClient.PutAsync("/api/Cars/BookingCar?id=" + carId, JsonContent.Create(""));
            }
            else
            {
                response = await _httpClient.PutAsync("/api/Cars/CancelBookingCar?id=" + carId, JsonContent.Create(""));
            }
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            isRent = JsonSerializer.Deserialize<bool>(responseBody);
            return isRent;
        }
    }
}
