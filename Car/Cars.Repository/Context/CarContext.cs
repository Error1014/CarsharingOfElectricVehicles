using Cars.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Context
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }
        public DbSet<BrandModel> BrandModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

    }
}
