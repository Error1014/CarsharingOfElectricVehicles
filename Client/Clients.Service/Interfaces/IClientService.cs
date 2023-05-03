using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Interfaces
{
    public interface IClientService
    {
        Task<ClientContactDTO> GetClient(Guid Id);
        Task<IEnumerable<ClientContactDTO>> GetClients(PageFilter pageFilter);
        Task AddClient(ClientContactDTO clientDTO);
        Task UpdateClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(Guid id, ClientContactDTO clientDTO);
        Task RemoveClient(Guid Id);
    }
}
