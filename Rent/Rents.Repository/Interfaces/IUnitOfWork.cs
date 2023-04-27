using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRentRepository Rents { get; }
        IRentChequeRepository RentCheques { get; }
        ITariffRepository Tariffs { get; }
        int Complete();
    }
}
