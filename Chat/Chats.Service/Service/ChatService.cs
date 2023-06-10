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

namespace Chats.Service.Service
{
    public class ChatService: IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IUserSessionGetter _userSessionGetter;
        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;
        }

        public async Task<Guid> AddChat()
        {
            var chat = await _unitOfWork.Chats.Find(x=>x.ClientId == _userSessionGetter.UserId);
            if (chat!=null)
            {
                throw new BadRequestException("Чат для данного пользователя уже есть"); 
            }
            chat = new Chat();
            chat.ClientId = _userSessionGetter.UserId;  
            await _unitOfWork.Chats.AddEntities(chat);
            await _unitOfWork.Chats.SaveChanges();
            return chat.Id;
        }

        public async Task<ChatDTO> GetChat(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetEntity(id);
            if (chat == null)
            {
                throw new NotFoundException("Чат не найден");
            }
            var result = _map.Map<ChatDTO>(chat);
            return result;
        }
        public async Task<ChatDTO> GetChat()
        {
            var chat = await _unitOfWork.Chats.GetEntity(_userSessionGetter.UserId);
            if (chat == null)
            {
                if (_userSessionGetter.Role=="Client")
                {
                    chat = new Chat();
                    chat.ClientId = _userSessionGetter.UserId;
                    await _unitOfWork.Chats.AddEntities(chat);
                    await _unitOfWork.Chats.SaveChanges();
                }
            }
            var result = _map.Map<ChatDTO>(chat);
            return result;
        }

        public async Task<Dictionary<Guid, ChatDTO>> GetChats(PageFilter pageFilter)
        {
            var chats = await _unitOfWork.Chats.GetPage(pageFilter);
            Dictionary<Guid, ChatDTO> result = new Dictionary<Guid, ChatDTO>();
            foreach (var item in chats)
            {
                result.Add(item.Id, _map.Map<ChatDTO>(item));
            }
            return result;
        }

        public async Task RemoveChat(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetEntity(id);
            if (chat == null)
            {
                throw new NotFoundException("Чат не найден");
            }
            _unitOfWork.Chats.RemoveEntities(chat);
            await _unitOfWork.Chats.SaveChanges();
        }

        public async Task UpdateChat(Guid id, ChatDTO chatDTO)
        {
            var chat = await _unitOfWork.Chats.GetEntity(id);
            if (chat == null)
            {
                throw new NotFoundException("Чат не найден");
            }
            var result = _map.Map<Chat>(chatDTO);
            result.Id = chat.Id;
            _unitOfWork.Chats.UpdateEntities(result);
            await _unitOfWork.Chats.SaveChanges();
        }
    }
}
