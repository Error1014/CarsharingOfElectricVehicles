using Infrastructure.Attributes;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Service.Interfaces;

namespace Subscriptions.Api.Controllers
{
    public class ClientSubscriptionsController : BaseApiController
    {
        private readonly IClientSubscriptionService _clientSubscriptionService;
        public ClientSubscriptionsController(IClientSubscriptionService clientSubscriptionService)
        {
            _clientSubscriptionService = clientSubscriptionService;
        }

        [RoleAuthorize("Client")]
        [HttpGet]
        public async Task<IActionResult> GetActualSubscription()
        {
            var result = await _clientSubscriptionService.GetActualSubscription();
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
