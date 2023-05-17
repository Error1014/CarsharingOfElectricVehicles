using Clients.Repository.Entities;
using Infrastructure.Repository;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Interfaces
{
    public interface IClientSubscriptionRepository : IRepository<ClientSubscription, Guid>
    {
        Task<ClientSubscription> GetActualSubsciption(Guid clientId);
    }
}
