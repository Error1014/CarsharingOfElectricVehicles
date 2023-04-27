using Cars.Repository.Entities;
using Infrastructure.DTO;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Interfaces
{
    public interface IBrandModelRepository : IRepository<BrandModel, Guid>
    {
        Task<IEnumerable<string>> GetBrands();
        Task<IEnumerable<string>> GetModels(string brand);
        Task<BrandModel> GetBrandModel(BrandModelDTO brandModelDTO);
    }
}
