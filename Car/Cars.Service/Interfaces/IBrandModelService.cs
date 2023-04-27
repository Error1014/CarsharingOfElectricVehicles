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
        Task<IEnumerable<string>> GetBrands();
        Task<IEnumerable<string>> GetModels(string brand);
        Task<IEnumerable<BrandModelDTO>> GetBrandModels();
        Task AddBrandModel(BrandModelDTO brandModelDTO);
        Task UpdateBrandModel(BrandModelDTO brandModelDTO);
        Task RemodeBrandModel(Guid Id);
    }
}
