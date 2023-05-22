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
    public class CharacteristicService: ICharacteristicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _map;
        public CharacteristicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task AddCharacteristic(CharacteristicDTO characteristicDTO)
        {
            var characteristic = await _unitOfWork.Characteristics.Find(x => x.Name == characteristicDTO.Name);
            if (characteristic != null)
            {
                throw new BadRequestException("Такая характеристика уже существует");
            }
            characteristic = _map.Map<Characteristic>(characteristicDTO);
            await _unitOfWork.Characteristics.AddEntities(characteristic);
            await _unitOfWork.Characteristics.SaveChanges();
        }

        public async Task<IEnumerable<CharacteristicDTO>> GetCharacteristics()
        {
            var list = await _unitOfWork.Characteristics.GetAll();
            var result = _map.Map<IEnumerable<CharacteristicDTO>>(list);
            return result;
        }

        public async Task RemoveCharacteristic(Guid id)
        {
            var characterictic = await _unitOfWork.Characteristics.GetEntity(id);
            _unitOfWork.Characteristics.RemoveEntities(characterictic);
            await _unitOfWork.Characteristics.SaveChanges();
        }

        public async Task UpdateCharacteristic(Guid id, CharacteristicDTO characteristicDTO)
        {
            var characteristic = _map.Map<Characteristic>(characteristicDTO);
            characteristic.Id = id;
            _unitOfWork.Characteristics.UpdateEntities(characteristic);
            await _unitOfWork.Characteristics.SaveChanges();
        }
    }
}
