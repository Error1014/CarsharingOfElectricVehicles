﻿using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Service.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDTO> GetActualSubscription();
        Task<SubscriptionDTO> GetSubscription(Guid id);
        Task<Dictionary<Guid , SubscriptionDTO>> GetSubscriptions(DefoltFilter pageFilter);
        Task<Guid> AddSubscripton(SubscriptionDTO subscriptionDTO);
        Task UpdateSubscripton(Guid id, SubscriptionDTO subscriptionDTO);
        Task RemoveSubscription(Guid id);

    }
}
