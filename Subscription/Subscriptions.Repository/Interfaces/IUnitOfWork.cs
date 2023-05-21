using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISubscriptionRepository Subscriptions { get; }
        IClientSubscriptionRepository ClientSubscriptions { get; }
        int Complete();
    }
}
