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
    public class ClientSubscriptionRepository : Repository<ClientSubscription, Guid>, IClientSubscriptionRepository
    {
        public ClientSubscriptionRepository(DbContext context) : base(context)
        {
        }

        public async Task<ClientSubscription> GetActualSubsciption(Guid clientId)
        {
            var result = await Set.Where(x => x.ClientId == clientId).OrderBy(x => x.DateSubscription).LastOrDefaultAsync();
            if (result == null) return null;
            else if (result.DateSubscription.AddMonths(1) > DateTime.Today) return result;
            else return null;
        }
    }
}
