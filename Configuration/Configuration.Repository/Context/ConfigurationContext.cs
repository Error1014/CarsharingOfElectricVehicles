using Configuration.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Context
{
    public class ConfigurationContext:DbContext
    {
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {

        }
        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
    }
}
