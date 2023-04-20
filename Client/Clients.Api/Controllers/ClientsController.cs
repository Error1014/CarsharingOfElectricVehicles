using Clients.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Api.Controllers
{
    public class ClientsController : BaseApiController
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [RoleAuthorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _clientService.GetClient(id);
            return Ok(client);
        }
        [RoleAuthorize("Admin Operator")]
        [HttpGet(nameof(GetClients))]
        public async Task<IActionResult> GetClients([FromQuery]PageFilter pageFilter)
        {
            var list = await _clientService.GetClients(pageFilter);
            return Ok(list);
        }

        [RoleAuthorize("Operator")]
        [HttpPost]
        public async Task<IActionResult> Registration(ClientDTO clientDTO)
        {
            await _clientService.AddClient(clientDTO);
            return Ok();
        }
        [RoleAuthorize("Operator")]
        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientDTO clientDTO)
        {
            await _clientService.UpdateClient(clientDTO);
            return Ok();
        }
        [RoleAuthorize("Client")]
        [HttpDelete]
        public async Task<IActionResult> RemoveClient(Guid Id)
        {
            await _clientService.RemoveClient(Id);
            return Ok();
        }
    }
}
