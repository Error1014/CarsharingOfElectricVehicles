using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Exceptions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
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
        private readonly UriEndPoint _getBalanceEndPoint;
        public ClientSubscriptionService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
            _getBalanceEndPoint = configuration.GetSection("EndPoint:GetBalance").Get<UriEndPoint>();
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
            var subscription = await _unitOfWork.Subscriptions.GetEntity(subscribleDTO.SubscriptionId);
            if (subscription == null)
            {
                throw new NotFoundException("Абонимент не найден");
            }
            var balance = await GetBalance();
            if (balance<subscription.Price)
            {
                throw new BadRequestException("Недостаточно средств");
            }
            var subscrible= new ClientSubscription(_userSessionGetter.UserId, subscribleDTO.SubscriptionId, subscribleDTO.QuntityMonths);
            await _unitOfWork.ClientSubscriptions.AddEntities(subscrible);
            await _unitOfWork.ClientSubscriptions.SaveChanges();
        }

        private async Task<decimal?> GetBalance()
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_getBalanceEndPoint.BaseAddress);
            var response = await _httpClient.GetAsync(_getBalanceEndPoint.Uri + _userSessionGetter.UserId);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            decimal? balance = JsonSerializer.Deserialize<decimal?>(responseBody);
            return balance;
        }

    }
}
