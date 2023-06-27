using Infrastructure.Attributes;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Service.Interfaces;
using Subscriptions.Service.Services;

namespace Subscriptions.Api.Controllers
{
    public class ClientSubscriptionsController : BaseApiController
    {
        private readonly IClientSubscriptionService _clientSubscriptionService;
        private readonly ISubscriptionService _subscriptionService;
        public ClientSubscriptionsController(IClientSubscriptionService clientSubscriptionService, ISubscriptionService subscriptionService)
        {
            _clientSubscriptionService = clientSubscriptionService;
            _subscriptionService = subscriptionService;
        }

        [RoleAuthorize("Client")]
        [HttpGet]
        public async Task<IActionResult> GetActualSubscription()
        {
            var result = await _clientSubscriptionService.GetActualSubscription();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActualSubscription(Guid id)
        {
            var result = await _clientSubscriptionService.GetActualSubscription(id);
            return Ok(result);
        }

        [RoleAuthorize("Client")]
        [HttpPost]
        public async Task<IActionResult> AddClientSubscribtion([FromQuery] SubscribleDTO subscribleDTO)
        {
            var id = await _clientSubscriptionService.Subscribe(subscribleDTO);
            return Created(new Uri("/api/ClientSubscriptions", UriKind.Relative), id);
        }

    }
}
