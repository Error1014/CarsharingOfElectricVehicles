using AutoMapper;
using Cars.Repository.Interfaces;
using Cars.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers
{
    public class BrandModelsController : BaseApiController
    {
        private readonly IBrandModelService _brandModelService;
        private readonly IMapper _map;

        public BrandModelsController(IBrandModelService brandModelService, IMapper mapper)
        {
            _brandModelService = brandModelService;
            _map = mapper;
        }

        [HttpGet(nameof(GetBrands))]
        public async Task<IEnumerable<string>> GetBrands()
        {
            return await _brandModelService.GetBrands();
        }

        [HttpGet(nameof(GetModels))]
        public async Task<IEnumerable<string>> GetModels(string brand)
        {
            return await _brandModelService.GetModels(brand);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpPost(nameof(AddBrandModel))]
        public async Task<IActionResult> AddBrandModel(BrandModelDTO brandModel)
        {
            await _brandModelService.AddBrandModel(brandModel);
            return Ok();
        }
    }
}
