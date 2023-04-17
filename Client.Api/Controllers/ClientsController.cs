using Clients.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clients.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _clientService.GetClient(id);
            return Ok(client);
        }
        [HttpGet(nameof(GetClients))]
        public async Task<IActionResult> GetClients()
        {
            var list = await _clientService.GetClients(new PageFilter(1, 10));
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddClient(ClientDTO clientDTO)
        {
            await _clientService.AddClient(clientDTO);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientDTO clientDTO)
        {
            await _clientService.UpdateClient(clientDTO);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveClient(Guid Id)
        {
            await _clientService.RemoveClient(Id);
            return Ok();
        }
    }
}
