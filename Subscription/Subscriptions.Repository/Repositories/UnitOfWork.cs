using Microsoft.EntityFrameworkCore;
using Subscriptions.Repository.Context;
using Subscriptions.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(SubscriptionContext context)
        {
            _dbContext = context;
            Subscriptions = new SubscriptionRepository(context);
            ClientSubscriptions = new ClientSubscriptionRepository(context);
        }




        public ISubscriptionRepository Subscriptions { get; private set; }
        public IClientSubscriptionRepository ClientSubscriptions { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
