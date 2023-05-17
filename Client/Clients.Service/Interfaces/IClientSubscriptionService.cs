﻿using Infrastructure.DTO.ClientDTOs;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Interfaces
{
    public interface IClientSubscriptionService
    {
        Task<ClientSubscriptionDTO> GetActualSubscription();
        Task Subscribe(SubscribleDTO subscribleDTO);
    }
}
