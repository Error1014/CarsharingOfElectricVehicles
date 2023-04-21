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
            var brandModel = await _unitOfWork.BrandModels.Find(x => x.Brand == brandModelDTO.Brand && x.Model == brandModelDTO.Model);
            if (brandModel != null)
            {
                throw new DublicateException("Модель этой марки уже есть в базе данных");
            }
            await _unitOfWork.BrandModels.AddEntities(brandModel);
            await _unitOfWork.BrandModels.SaveChanges();
        }

        public async Task<IEnumerable<BrandModelDTO>> GetBrands()
        {
            var list = await _unitOfWork.BrandModels.GetAll();
            var result = _map.Map<IEnumerable<BrandModelDTO>>(list);
            return result;
        }

        public async Task RemodeBrandModel(Guid Id)
        {
            var brandModel = await _unitOfWork.BrandModels.GetEntity(Id);
            if (brandModel == null)
            {
                throw new NotFoundException("Модели нет в базе данных");
            }
            _unitOfWork.BrandModels.RemoveEntities(brandModel);
            await _unitOfWork.BrandModels.SaveChanges();
        }

        public async Task UpdateBrandModel(BrandModelDTO brandModelDTO)
        {
            var brandModel = _map.Map<BrandModel>(brandModelDTO);
            _unitOfWork.BrandModels.UpdateEntities(brandModel);
            await _unitOfWork.BrandModels.SaveChanges();
        }
    }
}
