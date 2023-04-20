using Configuration.Repository.Context;
using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Repositories
{
    public class ConfigurationItemRepository : Repository<ConfigurationItem, Guid>, IConfigurationItemRepository
{
        private readonly ConfigurationContext _configurationContext;
        public ConfigurationItemRepository(ConfigurationContext context) : base(context)
        {
            _configurationContext = context;
        }

    }
}
