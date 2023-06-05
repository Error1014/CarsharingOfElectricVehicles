using Cars.Repository.Entities;
using Infrastructure.Filters;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Interfaces
{
    public interface ICarRepository : IRepository<Car, Guid>
    {
        Task<IEnumerable<Car>> GetCars(CarFilter carFilter);
    }
}
