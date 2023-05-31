using Infrastructure.DTO;
using Infrastructure.Filters;

namespace Chats.Service.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO>> GetMessages(Guid chatId, PageFilter pageFilter);
        Task SendMessage(MessageDTO messageDTO);
        Task SendMessageClient(string text);
        Task SendMessageOperator(Guid clientId, string text);
    }
}
