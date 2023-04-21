using Cars.Repository.Entities;
using Cars.Repository.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Repositories
{
    public class BrandModelRepository : Repository<BrandModel, Guid>, IBrandModelRepository
    {
        public BrandModelRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<string>> GetBrands()
        {
            return Set.Distinct().Select(x=>x.Brand);
        }
        public async Task<IEnumerable<string>> GetModels(string brand)
        {
            return Set.Where(x=>x.Brand == brand).Distinct().Select(x => x.Brand);
        }
        public async Task<BrandModel> GetBrandModel(BrandModelDTO brandModelDTO)
        {
            return await Set.FirstOrDefaultAsync(x => x.Brand == brandModelDTO.Brand && x.Model == brandModelDTO.Model);
        }

    }
}
