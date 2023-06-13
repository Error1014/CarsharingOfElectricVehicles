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
        [RoleAuthorize("Admin Operator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _clientService.GetClient(id);
            return Ok(client);
        }
        //[RoleAuthorize("Admin Operator")]
        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] ClientFilter clientFilter)
        {
            var list = await _clientService.GetClients(clientFilter);
            return Ok(list);
        }
       
        [HttpPost("{id}")]
        public async Task<IActionResult> AddClient(Guid id, ClientDocumentDTO clientDTO)
        {
           await _clientService.AddClient(id, clientDTO);
            return Created(new Uri("/api/Configurations", UriKind.Relative), id);
        }
        [RoleAuthorize("Operator")]
        [HttpPut(nameof(UpdateClientByOperator) + "/{id}")]
        public async Task<IActionResult> UpdateClientByOperator(Guid id, ClientDocumentDTO clientDTO)
        {
            await _clientService.UpdateClient(id, clientDTO);
            return NoContent();
        }
        [RoleAuthorize("Client")]
        [HttpPut(nameof(UpdateClientByClient))]
        public async Task<IActionResult> UpdateClientByClient(ClientContactDTO clientDTO)
        {
            await _clientService.UpdateClient(clientDTO);
            return NoContent();
        }

        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClient(Guid Id)
        {
            await _clientService.RemoveClient(Id);
            return NoContent();
        }
    }
}
