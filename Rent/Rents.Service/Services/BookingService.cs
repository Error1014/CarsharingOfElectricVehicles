using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
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
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Rents.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        private HttpClient _httpClient;
        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;
            _httpClient = new HttpClient();
        }
        public async Task AddBooking(BookingDTO bookingDTO)
        {
            var lastBoocking = await _unitOfWork.Bookings.GetLastBooking(_userSessionGetter.UserId);
            if (lastBoocking != null)
            {
                var chek = await _unitOfWork.RentCheques.Find(x => x.RentId == lastBoocking.Id);
                if (chek == null)
                {
                    throw new NotFoundException("Вы уже арендуете авто, и не можете начать новую аренду");//Заменить ошибку
                }
            }
            bool isRent = await RentCar(bookingDTO.CarId);
            if (isRent)
            {
                var booking = _map.Map<Booking>(bookingDTO);
                booking.ClientId = _userSessionGetter.UserId;
                booking.DateTimeBeginBoocking = DateTime.Now;
                await _unitOfWork.Bookings.AddEntities(booking);
                await _unitOfWork.Bookings.SaveChanges();
            }
            
        }

        //private async Task<bool> CheckIsRentCar(Guid carId)
        //{
        //    UriEndPoint uriEndPoint = new UriEndPoint(); //Заменить на взятие адреса из конфигурации
        //    _httpClient.BaseAddress = new Uri("https://localhost:7215");
        //    HttpResponseMessage response = await _httpClient.GetAsync("/api/Cars/GetCarIsRent?id="+carId);
        //    response.EnsureSuccessStatusCode();
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var isRent = JsonSerializer.Deserialize<bool>(responseBody);
        //    return isRent;
        //}
        private async Task<bool> RentCar(Guid carId)
        {
            UriEndPoint uriEndPoint = new UriEndPoint(); //Заменить на взятие адреса из конфигурации
            _httpClient.BaseAddress = new Uri("https://localhost:7215");
            HttpResponseMessage response = await _httpClient.PutAsync("/api/Cars/UpdateRentCar?id="+carId+"&isRent="+true, JsonContent.Create(""));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var isRent = JsonSerializer.Deserialize<bool>(responseBody);
            return isRent;
        }
        public async Task RemoveBooking(Guid id)
        {
            var booking = await _unitOfWork.Bookings.GetEntity(id);
            if (booking == null)
            {
                throw new NotFoundException("Не найден");
            }
            _unitOfWork.Bookings.RemoveEntities(booking);
            await _unitOfWork.Bookings.SaveChanges();
        }

        public async Task<BookingDTO> GetBooking(Guid id)
        {
            var bookin = await _unitOfWork.Bookings.GetEntity(id);
            var result = _map.Map<BookingDTO>(bookin);
            return result;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookings()
        {
            var bookin = await _unitOfWork.Bookings.GetAll();
            var result = _map.Map<IEnumerable<BookingDTO>>(bookin);
            return result;
        }
        public async Task<IEnumerable<BookingDTO>> GetBookingsByClient()
        {
            var boockin = await _unitOfWork.Bookings.GetAllBookingByClient(_userSessionGetter.UserId);
            var result = _map.Map<IEnumerable<BookingDTO>>(boockin);
            return result;
        }

        public async Task UpdateBooking(Guid id, BookingDTO bookingDTO)
        {
            var booking = _map.Map<Booking>(bookingDTO);
            booking.Id = id;
            booking.ClientId = _userSessionGetter.UserId;
            _unitOfWork.Bookings.UpdateEntities(booking);
            await _unitOfWork.Bookings.SaveChanges();
        }
    }
}
