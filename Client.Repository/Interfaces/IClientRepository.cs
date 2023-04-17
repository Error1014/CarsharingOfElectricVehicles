using Clients.Repository.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
    }
}
