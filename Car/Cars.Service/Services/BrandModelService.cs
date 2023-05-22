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
    public class BrandModelService : IBrandModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        public BrandModelService(IUnitOfWork unitOfWork, IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }
        public async Task AddBrandModel(BrandModelDTO brandModelDTO)
        {
            var brandModel = _map.Map<BrandModel>(brandModelDTO);
            await _unitOfWork.BrandModels.AddEntities(brandModel);
            await _unitOfWork.BrandModels.SaveChanges();
        }
        public async Task<BrandModelDTO> GetBrandModel(Guid id)
        {
            var brandModel = await CheckInDb(id);
            var result = _map.Map<BrandModelDTO>(brandModel);
            return result;
        }
        public async Task<Dictionary<Guid, BrandModelDTO>> GetBrandModels()
        {
            var list = await _unitOfWork.BrandModels.GetAll();
            Dictionary<Guid, BrandModelDTO> result = new Dictionary<Guid, BrandModelDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<BrandModelDTO>(item));
            }
            return result;
        }

        public async Task RemodeBrandModel(Guid id)
        {
            var brandModel = await CheckInDb(id);
            _unitOfWork.BrandModels.RemoveEntities(brandModel);
            await _unitOfWork.BrandModels.SaveChanges();
        }

        public async Task UpdateBrandModel(Guid id, BrandModelDTO brandModelDTO)
        {
            var brandModel = await CheckInDb(id);
            var result = _map.Map<BrandModel>(brandModelDTO);
            result.Id = id;
            _unitOfWork.BrandModels.UpdateEntities(result);
            await _unitOfWork.BrandModels.SaveChanges();
        }

        private async Task<BrandModel> CheckInDb(Guid id)
        {
            var brandModel = await _unitOfWork.BrandModels.GetEntity(id);
            if (brandModel == null)
            {
                throw new NotFoundException("Модели нет в базе данных");
            }
            return brandModel;
        }
    }
}
