using Infrastructure.DTO;
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
        Task<FIODTO> GetFIO();
        Task<ClientContactDTO> GetClient(Guid Id);
        Task<Dictionary<Guid, ClientDTO>> GetClients(ClientFilter clientFilter);
        Task AddClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(ClientContactDTO clientDTO);
        Task RemoveClient(Guid Id);
    }
}
