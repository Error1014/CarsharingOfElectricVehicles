using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       IClientRepository ClientRepositories { get; }
        int Complete();
    }
}
