using Clients.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Repository.Entities;

namespace Clients.Api.Controllers
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
            await _clientSubscriptionService.Subscribe(subscribleDTO);
            return Ok();
        }

    }
}
