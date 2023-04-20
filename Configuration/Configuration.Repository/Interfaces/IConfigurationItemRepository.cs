using Configuration.Repository.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Interfaces
{
    public interface IConfigurationItemRepository : IRepository<ConfigurationItem, Guid>
    {
    }
}
