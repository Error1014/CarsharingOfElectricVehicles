using Microsoft.EntityFrameworkCore;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Context
{
    public class SubscriptionContext : DbContext
    {
        public SubscriptionContext(DbContextOptions<SubscriptionContext> options) : base(options)
        {

        }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ClientSubscription> ClientSubscriptions { get; set; }

    }
}
