using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Subscriptions.Repository.Entities;
using Subscriptions.Repository.Interfaces;
using Subscriptions.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Subscriptions.Service.Services
{
    public class ClientSubscriptionService : IClientSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        public ClientSubscriptionService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
        }

        public async Task<ClientSubscriptionDTO> GetActualSubscription()
        {
            var clientSub = await _unitOfWork.ClientSubscriptions.GetActualSubsciption(_userSessionGetter.UserId);
            if (clientSub == null)
            {
                throw new NotFoundException("У вас нет действующего абониметна");
            }
            var result = _map.Map<ClientSubscriptionDTO>(clientSub);
            return result;
        }

        public async Task Subscribe(SubscribleDTO subscribleDTO)
        {
            var subscriptionDTO = await _unitOfWork.Subscriptions.GetEntity(subscribleDTO.SubscriptionId);
            if (subscriptionDTO == null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            var subscription = new ClientSubscription(_userSessionGetter.UserId, subscribleDTO.SubscriptionId, subscribleDTO.QuntityMonths);
            await _unitOfWork.ClientSubscriptions.AddEntities(subscription);
            await _unitOfWork.ClientSubscriptions.SaveChanges();
        }

    }
}
