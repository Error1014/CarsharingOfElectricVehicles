using Microsoft.EntityFrameworkCore;
using Rents.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Context
{
    public class RentContext:DbContext
    {
        public RentContext(DbContextOptions<RentContext> options) : base(options)
        {

        }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentCheque> RentCheques { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
    }
}
