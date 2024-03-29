﻿using Chats.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chats.Api.Controllers
{
    public class ChatsController : BaseApiController
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(Guid id)
        {
            var result = await _chatService.GetChat(id);
            return Ok(result);
        }
        [HttpGet("my")]
        public async Task<IActionResult> GetChat()
        {
            var result = await _chatService.GetChat();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetChats([FromQuery]DefoltFilter pageFilter)
        {
            var result = await _chatService.GetChats(pageFilter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddChat()
        {
            var id =  await _chatService.AddChat();
            return Created(new Uri("/api/Chats", UriKind.Relative), id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateChat(Guid id, ChatDTO chatDTO)
        {
            await _chatService.UpdateChat(id, chatDTO);
            return NoContent();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveChat(Guid id)
        {
            await _chatService.RemoveChat(id);
            return NoContent();
        }
    }
}
