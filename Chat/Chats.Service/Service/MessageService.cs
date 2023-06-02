using AutoMapper;
using Chats.Repository.Entities;
using Chats.Repository.Interfaces;
using Chats.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            foreach (var item in list)
            {
                File.WriteAllBytes(item.FileName, item.FileData);
            }
            var result = _map.Map<IEnumerable<MessageDTO>>(list);

            return result;
        }
        public async Task SendMessage(MessageDTO messageDTO)
        {
            var message = _map.Map<Message>(messageDTO);
            message.DateTime = DateTime.Now;
            message.FileName = messageDTO.File.FileName;
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(messageDTO.File.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)messageDTO.File.Length);
            }
            message.FileData = fileData;
            await _unitOfWork.Messages.AddEntities(message);
            await _unitOfWork.Messages.SaveChanges();
        }
    }

}
