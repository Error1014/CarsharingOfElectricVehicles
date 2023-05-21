using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Repository.Entities;
using Subscriptions.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Repositories
{
    public class SubscriptionRepository : Repository<Subscription, Guid>, ISubscriptionRepository
    {
        public SubscriptionRepository(DbContext context) : base(context)
        {
        }
    }
}
