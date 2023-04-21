using Cars.Repository.Entities;
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
    }
}
