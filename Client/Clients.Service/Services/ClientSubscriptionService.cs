using AutoMapper;
using Clients.Repository.Interfaces;
using Clients.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Subscriptions.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clients.Service.Services
{
    public class ClientSubscriptionService : IClientSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        private HttpClient _httpClient;
        public ClientSubscriptionService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
            _httpClient = new HttpClient();
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
            _httpClient.BaseAddress = new Uri("https://localhost:7217/");
            var response = await _httpClient.GetAsync($"api/Subscriptions/"+ subscribleDTO.SubscriptionId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var subscriptionDTO = JsonSerializer.Deserialize<SubscriptionDTO>(responseBody, options);
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
