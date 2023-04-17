using Infrastructure.DTO;
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
        Task<ClientDTO> GetClient(Guid Id);
        Task<IEnumerable<ClientDTO>> GetClients(PageFilter pageFilter);
        Task AddClient(ClientDTO clientDTO);
        Task UpdateClient(ClientDTO clientDTO);
        Task RemoveClient(Guid Id);
    }
}
