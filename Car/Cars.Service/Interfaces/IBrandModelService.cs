using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Interfaces
{
    public interface IBrandModelService
    {
        Task<BrandModelDTO> GetBrandModel(Guid id);
        Task<Dictionary<Guid,BrandModelDTO>> GetBrandModels();
        Task<Guid> AddBrandModel(BrandModelDTO brandModelDTO);
        Task UpdateBrandModel(Guid id,BrandModelDTO brandModelDTO);
        Task RemodeBrandModel(Guid Id);
    }
}
