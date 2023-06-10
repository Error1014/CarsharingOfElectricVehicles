using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using Subscriptions.Repository.Entities;
using Subscriptions.Repository.Interfaces;
using Subscriptions.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Service.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        public SubscriptionService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
        }

        public async Task<Guid> AddSubscripton(SubscriptionDTO subscriptionDTO)
        {
            var subscription = _map.Map<Subscription>(subscriptionDTO);
            await _unitOfWork.Subscriptions.AddEntities(subscription);
            await _unitOfWork.Subscriptions.SaveChanges();
            return subscription.Id;
        }
        public async Task<SubscriptionDTO> GetActualSubscription()
        {
            var clientSubscription = await _unitOfWork.ClientSubscriptions.GetActualSubsciption(_userSessionGetter.UserId);
            if (clientSubscription == null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            var subscription = await _unitOfWork.Subscriptions.GetEntity(clientSubscription.SubscriptionId);
            if (clientSubscription == null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            var result = _map.Map<SubscriptionDTO>(subscription);
            return result;
        }
        public async Task<SubscriptionDTO> GetSubscription(Guid id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetEntity(id);
            if (subscription==null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            var result = _map.Map<SubscriptionDTO>(subscription);
            return result;
        }

        public async Task<Dictionary<Guid, SubscriptionDTO>> GetSubscriptions(PageFilter pageFilter)
        {
            var subscription = await _unitOfWork.Subscriptions.GetPage(pageFilter);
            Dictionary<Guid, SubscriptionDTO> result = new Dictionary<Guid, SubscriptionDTO>();
            foreach (var item in subscription)
            {
                result.Add(item.Id, _map.Map<SubscriptionDTO>(item));
            }
            return result;
        }

        public async Task RemoveSubscription(Guid id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetEntity(id);
            if (subscription == null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            _unitOfWork.Subscriptions.RemoveEntities(subscription);
            await _unitOfWork.Subscriptions.SaveChanges();
        }

        public async Task UpdateSubscripton(Guid id, SubscriptionDTO subscriptionDTO)
        {
            var subscription = _map.Map<Subscription>(subscriptionDTO);
            subscription.Id = id;
            _unitOfWork.Subscriptions.UpdateEntities(subscription);
            await _unitOfWork.Subscriptions.SaveChanges();
        }
    }
}
