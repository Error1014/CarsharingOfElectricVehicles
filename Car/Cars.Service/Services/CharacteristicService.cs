using AutoMapper;
using Cars.Repository.Entities;
using Cars.Repository.Interfaces;
using Cars.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Services
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _map;
        public CharacteristicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task<Guid> AddCharacteristicByCarId(CharacteristicDTO characteristicDTO)
        {
            var characteristic = await _unitOfWork.Characteristics.Find(x => x.CarId == characteristicDTO.CarId);
            if (characteristic != null)
            {
                throw new BadRequestException("Характеристика на данный автомобиль уже есть");
            }
            else
            {
                characteristic = _map.Map<Characteristic>(characteristicDTO);
                await _unitOfWork.Characteristics.AddEntities(characteristic);
                await _unitOfWork.Characteristics.SaveChanges();
            }
            return characteristic.Id;
        }

        public async Task RemoveCharacteristicByCarId(Guid carId)
        {
            var characteristic = await _unitOfWork.Characteristics.Find(x => x.CarId == carId);
            if (characteristic == null)
            {
                throw new NotFoundException("Характеристика не найдена");
            }
            else
            {
                _unitOfWork.Characteristics.RemoveEntities(characteristic);
                await _unitOfWork.Characteristics.SaveChanges();
            }
        }

        public async Task<CharacteristicDTO> GetCharacteristicByCarId(Guid carId)
        {
            var characteristic = await _unitOfWork.Characteristics.Find(x => x.CarId == carId);
            if (characteristic == null)
            {
                throw new NotFoundException("Характеристика не найдена");
            }
            var result = _map.Map<CharacteristicDTO>(characteristic);
            return result;
        }

        public async Task UpdateCharacteristicByCarId(Guid carId, CharacteristicDTO characteristicDTO)
        {
            var characteristic = await _unitOfWork.Characteristics.Find(x => x.CarId == carId);
            if (characteristic == null)
            {
                throw new NotFoundException("Характеристика не найдена");
            }
            else
            {
                characteristic = _map.Map<Characteristic>(characteristicDTO);
                characteristic.CarId = carId;
                _unitOfWork.Characteristics.UpdateEntities(characteristic);
                await _unitOfWork.Characteristics.SaveChanges();
            }
        }
    }
}
