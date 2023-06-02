using Infrastructure.DTO;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Http;

namespace Chats.Service.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO>> GetMessages(Guid chatId, PageFilter pageFilter);
        Task SendMessage(MessageDTO messageDTO);
    }
}
