using AutoMapper;
using Cars.Repository.Interfaces;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers
{
    public class BrandModelsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public BrandModelsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        [HttpGet(nameof(GetBrands))]
        public async Task<IEnumerable<string>> GetBrands()
        {
            return await _unitOfWork.BrandModels.GetBrands();
        }

        [HttpGet(nameof(GetModels))]
        public async Task<IEnumerable<string>> GetModels(string brand)
        {
            return await _unitOfWork.BrandModels.GetModels(brand);
        }
    }
}
