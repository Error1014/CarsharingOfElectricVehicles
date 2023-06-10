using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Rents.Repository.Entities;
using Rents.Repository.Interfaces;
using Rents.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Service.Services
{
    public class TariffService : ITariffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public TariffService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task<Guid> AddTariff(TariffDTO tariffDTO)
        {
            var tariff = _map.Map<Tariff>(tariffDTO);
            await _unitOfWork.Tariffs.AddEntities(tariff);
            await _unitOfWork.Tariffs.SaveChanges();
            return tariff.Id;
        }

        public async Task<TariffDTO> GetTariff(Guid Id)
        {
            var tariff = await _unitOfWork.Tariffs.GetEntity(Id);
            if (tariff == null)
            {
                throw new NotFoundException("Данный тариф не найден");
            }
            var result = _map.Map<TariffDTO>(tariff);
            return result;
        }

        public async Task<Dictionary<Guid, TariffDTO>> GetTariffs()
        {
            var list = await _unitOfWork.Tariffs.GetAll();
            Dictionary<Guid, TariffDTO> result =new Dictionary<Guid, TariffDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<TariffDTO>(item));
            }
            return result;
        }

        public async Task RemoveTariff(Guid id)
        {
            var tariff = await _unitOfWork.Tariffs.GetEntity(id);
            if (tariff == null)
            {
                throw new NotFoundException("Данный тариф не найден");
            }
            _unitOfWork.Tariffs.RemoveEntities(tariff);
            await _unitOfWork.Tariffs.SaveChanges();
        }

        public async Task UpdateTarif(Guid id, TariffDTO tariffDTO)
        {
            var tariff = await _unitOfWork.Tariffs.GetEntity(id);
            if (tariff == null)
            {
                throw new NotFoundException("Данный тариф не найден");
            }
            tariff = _map.Map<Tariff>(tariffDTO);
            tariff.Id = id;
            _unitOfWork.Tariffs.UpdateEntities(tariff);
            await _unitOfWork.Tariffs.SaveChanges();
        }
    }
}
