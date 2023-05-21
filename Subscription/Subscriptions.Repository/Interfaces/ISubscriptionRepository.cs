using Infrastructure.Repository;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription, Guid>
    {
    }
}
