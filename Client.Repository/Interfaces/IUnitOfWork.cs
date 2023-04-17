using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       IClientRepository Clients { get; }
        int Complete();
    }
}
