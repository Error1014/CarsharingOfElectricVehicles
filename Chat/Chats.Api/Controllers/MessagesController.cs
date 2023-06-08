using Chats.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chats.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(Guid chatId, [FromQuery] PageFilter pageFilter)
        {
            var result = await _messageService.GetMessages(chatId, pageFilter);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostMessages([FromForm]MessageDTO messageDTO)
        {
            var id = await _messageService.SendMessage(messageDTO);
            return Created(new Uri("/api/Messages", UriKind.Relative), id);
        }
    }
}
