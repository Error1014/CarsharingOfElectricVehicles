using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITariffRepository Tariffs { get; }
        IRentRepository Rents { get; }
        int Complete();
    }
}
