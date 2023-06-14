using Infrastructure.DTO.ClientDTOs;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Service.Interfaces
{
    public interface IClientSubscriptionService
    {
        Task<ClientSubscriptionDTO> GetActualSubscription();
        Task<ClientSubscriptionDTO> GetActualSubscription(Guid id);
        Task<Guid> Subscribe(SubscribleDTO subscribleDTO);
    }
}
