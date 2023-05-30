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
        Task<decimal?> GetBalance();
        Task<decimal?> GetBalance(Guid id);
        Task<ClientContactDTO> GetClient(Guid Id);
        Task<Dictionary<Guid, ClientContactDTO>> GetClients(PageFilter pageFilter);
        Task AddClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(Guid id,ClientDocumentDTO clientDTO);
        Task UpdateClient(ClientContactDTO clientDTO);
        Task UpdateBalance(decimal summ);
        Task RemoveClient(Guid Id);
    }
}
