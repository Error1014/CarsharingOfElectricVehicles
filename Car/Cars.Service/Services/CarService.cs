using AutoMapper;
using Cars.Repository.Entities;
using Cars.Repository.Interfaces;
using Cars.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task AddCar(CarDTO carDTO)
        {
            var car = _map.Map<Car>(carDTO);
            await _unitOfWork.Cars.AddEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }

        public async Task<CarDTO> GetCar(Guid id)
        {
            var car = await _unitOfWork.Cars.GetEntity(id);
            if (car == null)
            {
                throw new NotFoundException("Автомобиль не найден");
            }
            var result = _map.Map<CarDTO>(car);
            return result;
        }

        public async Task<Dictionary<Guid, CarDTO>> GetCars(PageFilter pageFilter)
        {
            var list = await _unitOfWork.Cars.GetPage(pageFilter);
            Dictionary<Guid, CarDTO> result = new Dictionary<Guid, CarDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<CarDTO>(item));
            }
            return result;
        }

        public async Task RemoveCar(Guid id)
        {
            var car = await _unitOfWork.Cars.GetEntity(id);
            _unitOfWork.Cars.RemoveEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }

        public async Task UpdateCar(Guid id, CarDTO carDTO)
        {
            var car = _map.Map<Car>(carDTO);
            car.Id = id;
            _unitOfWork.Cars.UpdateEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }
        public async Task UpdateCarRent(Guid id, bool isRent)
        {
            var car = await _unitOfWork.Cars.GetEntity(id);
            if (car.IsRent == true && isRent == true)
            {
                throw new NotFoundException("Машина уже арендована");
            }
            if (car.IsRepair && isRent == true)
            {
                throw new NotFoundException("Машина на техобслуживании");
            }
            car.IsRent = isRent;
            _unitOfWork.Cars.UpdateEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }
        public async Task BookingCar(Guid id)
        {
            var car = await _unitOfWork.Cars.GetEntity(id);
            if (car.IsRent == true)
            {
                throw new NotFoundException("Автомобиль уже арендован");
            }
            else if (car.IsRepair == true)
            {
                throw new NotFoundException("Автомобиль на данный момент на техобслуживании");
            }
            car.IsRent = true;
            _unitOfWork.Cars.UpdateEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }
        public async Task CancelBookingCar(Guid id)
        {
            var car = await _unitOfWork.Cars.GetEntity(id);
            if (car == null)
            {
                throw new NotFoundException("Автомобиль не найден");
            }
            car.IsRent = false;
            _unitOfWork.Cars.UpdateEntities(car);
            await _unitOfWork.Cars.SaveChanges();
        }
    }
}
