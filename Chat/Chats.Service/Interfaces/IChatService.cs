using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Service.Interfaces
{
    public interface IChatService
    {
        Task<ChatDTO> GetChat(Guid id);
        Task<ChatDTO> GetChat();
        Task<Dictionary<Guid, ChatDTO>> GetChats(PageFilter pageFilter);
        Task<Guid> AddChat();
        Task UpdateChat(Guid id, ChatDTO chat);
        Task RemoveChat(Guid id);

    }
}
