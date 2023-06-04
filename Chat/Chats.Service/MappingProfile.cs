using AutoMapper;
using Chats.Repository.Entities;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chats.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Chat, ChatDTO>();
            CreateMap<Chat, ChatDTO>().ReverseMap();
        }
    }
}
