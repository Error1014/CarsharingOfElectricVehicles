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
        Task<Dictionary<Guid, ClientContactDTO>> GetClients(PageFilter pageFilter);
        Task AddClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(Guid id, ClientContactDTO clientDTO);
        Task UpdateBalance(decimal summ);
        Task RemoveClient(Guid Id);
    }
}
