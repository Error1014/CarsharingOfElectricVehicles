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
        Task<IEnumerable<BrandModelDTO>> GetBrands();
        Task AddBrandModel(BrandModelDTO brandModelDTO);
        Task UpdateBrandModel(BrandModelDTO brandModelDTO);
        Task RemodeBrandModel(Guid Id);
    }
}
