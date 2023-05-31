using AutoMapper;
using Chats.Repository.Entities;
using Chats.Repository.Interfaces;
using Chats.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace Chats.Service.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;

        public MessageService(IUnitOfWork unitOfWork, IMapper map, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = map;
            _userSessionGetter = userSessionGetter;
        }
        public async Task<IEnumerable<MessageDTO>> GetMessages(Guid chatId, PageFilter pageFilter)
        {
            var list = await _unitOfWork.Messages.GetMessages(chatId, pageFilter);
            var result = _map.Map<IEnumerable<MessageDTO>>(list);
            return result;
        }
        public async Task SendMessage(MessageDTO messageDTO)
        {
            var message = _map.Map<Message>(messageDTO);
            await _unitOfWork.Messages.AddEntities(message);
            await _unitOfWork.Messages.SaveChanges();
        }
        public async Task SendMessageClient(string text)
        {
            var chat = await _unitOfWork.Chats.Find(x => x.ClientId == _userSessionGetter.UserId);
            if (chat == null) throw new NotFoundException("Чат не найден");
            var message = new Message(chat.Id, _userSessionGetter.UserId, null, text);
        }
        public async Task SendMessageOperator(Guid clientId, string text)
        {
            var chat = await _unitOfWork.Chats.Find(x => x.ClientId == clientId);
            if (chat == null) throw new NotFoundException("Чат не найден");
            var message = new Message(chat.Id, clientId, _userSessionGetter.UserId, text);
        }
    }
}
