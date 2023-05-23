using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Repository.Interfaces;
using Subscriptions.Service.Interfaces;

namespace Subscriptions.Api.Controllers
{
    public class SubscriptionsController : BaseApiController
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActualSubscription()
        {
            var result = await _subscriptionService.GetActualSubscription();
            return Ok(result);
        }
        [HttpGet("{/id}")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var result = await _subscriptionService.GetSubscription(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubscriptions([FromQuery] PageFilter pageFilter)
        {
            var result = await _subscriptionService.GetSubscriptions(pageFilter);
            return Ok(result);
        }
        [RoleAuthorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSubscription([FromQuery] SubscriptionDTO subscriptionDTO)
        {
            await _subscriptionService.AddSubscripton(subscriptionDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPut("{/id}")]
        public async Task<IActionResult> UpdateSubscription(Guid id, [FromQuery] SubscriptionDTO subscriptionDTO)
        {
            await _subscriptionService.UpdateSubscripton(id, subscriptionDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{/id}")]
        public async Task<IActionResult> RemoveSubscription(Guid id)
        {
            await _subscriptionService.RemoveSubscription(id);
            return Ok();
        }
    }
}
