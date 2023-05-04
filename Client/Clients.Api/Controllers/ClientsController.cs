using Clients.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO.ClientDTOs;
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
        [HttpPost("id")]
        public async Task<IActionResult> AddClient(Guid id, ClientDocumentDTO clientDTO)
        {
            await _clientService.AddClient(id, clientDTO);
            return Ok();
        }
        [RoleAuthorize("Operator")]
        [HttpPut(nameof(UpdateClientByOperator)+ "id")]
        public async Task<IActionResult> UpdateClientByOperator(Guid id, ClientDocumentDTO clientDTO)
        {
            await _clientService.UpdateClient(id,clientDTO);
            return Ok();
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(UpdateClientByClient)+"id")]
        public async Task<IActionResult> UpdateClientByClient(Guid id, ClientContactDTO clientDTO)
        {
            await _clientService.UpdateClient(id, clientDTO);
            return Ok();
        }

        [HttpPut(nameof(UpdateBalance) + "id")]
        public async Task<IActionResult> UpdateBalance(decimal summ)
        {
            await _clientService.UpdateBalance(summ);
            return Ok();
        }
        [RoleAuthorize("Client")]
        [HttpDelete("id")]
        public async Task<IActionResult> RemoveClient(Guid Id)
        {
            await _clientService.RemoveClient(Id);
            return Ok();
        }
    }
}
