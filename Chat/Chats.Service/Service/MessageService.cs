using AutoMapper;
using Chats.Repository.Entities;
using Chats.Repository.Interfaces;
using Chats.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
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
        private string[] permittedExtensions = { ".jpg", ".png", ".jpeg" };
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
        public async Task<Guid> SendMessage(MessageDTO messageDTO)
        {
            var message = _map.Map<Message>(messageDTO);
            message.DateTime = DateTime.Now;
            message.FileName = messageDTO.File.FileName;
            var ext = Path.GetExtension(message.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                throw new Exception("Неподдерживаемый формат");
            }
            //message.FileName = Path.GetRandomFileName();
            using (var memoryStream = new MemoryStream())
            {
                await messageDTO.File.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    message.FileData = memoryStream.ToArray();
                }
                else
                {
                    throw new Exception("Большой файл");
                }
            }
            await _unitOfWork.Messages.AddEntities(message);
            await _unitOfWork.Messages.SaveChanges();
            return message.Id;
        }
    }

}
