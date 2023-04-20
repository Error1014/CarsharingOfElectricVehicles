using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients.Repository.Entities;

namespace Clients.Repository.Context
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Passport> Passport { get; set; }
        public DbSet<DrivingLicense> DrivingLicense { get; set; }
    }
}
