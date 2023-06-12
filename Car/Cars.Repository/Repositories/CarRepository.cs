using Cars.Repository.Entities;
using Cars.Repository.Interfaces;
using Infrastructure.Filters;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Repositories
{
    public class CarRepository : Repository<Car, Guid>, ICarRepository
    {
        public CarRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Car>> GetCars(CarFilter carFilter)
        {
            var query = Set;
            if (!carFilter.Brand.IsNullOrEmpty())
            {
                query = query.Where(x => x.BrandModel.Brand == carFilter.Brand);
            }
            if (!carFilter.Model.IsNullOrEmpty())
            {
                query = query.Where(x => x.BrandModel.Brand == carFilter.Model);
            }
            if (carFilter.MinYearOfRelease.HasValue)
            {
                query = query.Where(x => x.Characteristic.YearOfRelease >= carFilter.MinYearOfRelease);
            }
            if (carFilter.MinYearOfRelease.HasValue)
            {
                query = query.Where(x => x.Characteristic.YearOfRelease <= carFilter.MaxYearOfRelease);
            }
            if (carFilter.MinMileage.HasValue)
            {
                query = query.Where(x => x.Characteristic.Mileage >= carFilter.MinMileage);
            }
            if (carFilter.MaxMileage.HasValue)
            {
                query = query.Where(x => x.Characteristic.Mileage <= carFilter.MaxMileage);
            }
            if (!carFilter.Transmission.IsNullOrEmpty())
            {
                query = query.Where(x => x.Characteristic.Transmission == carFilter.Transmission);
            }
            if (!carFilter.WheelDrive.IsNullOrEmpty())
            {
                query = query.Where(x => x.Characteristic.WheelDrive == carFilter.WheelDrive);
            }
            if (!carFilter.Rudder.IsNullOrEmpty())
            {
                query = query.Where(x => x.Characteristic.Rudder == carFilter.Rudder);
            }
            if (carFilter.IsRent.HasValue)
            {
                query = query.Where(x => x.IsRent == carFilter.IsRent);
            }
            if (carFilter.IsRepair.HasValue)
            {
                query = query.Where(x => x.IsRepair == carFilter.IsRepair);
            }
            query = query
                .OrderBy(x => x.Id)
                .Skip((carFilter.PageFilter.Offset - 1) * carFilter.PageFilter.SizePage)
                .Take(carFilter.PageFilter.SizePage);
            return await query.ToListAsync();
        }
    }
}
