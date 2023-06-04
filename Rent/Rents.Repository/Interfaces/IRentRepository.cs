using Infrastructure.Filters;
using Infrastructure.Repository;
using Rents.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Interfaces
{
    public interface IRentRepository : IRepository<Rent, Guid>
    {
        Task<Rent> GetActualRent(Guid clientId); 
        Task<IEnumerable<Rent>> GetRentHistoryPage(HistoryRentFilter historyRentFilter);
    }
}
