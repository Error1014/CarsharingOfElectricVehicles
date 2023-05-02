using Authorization.Repository.Entities;
using AutoMapper;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<LoginDTO, UserDTO>();
            CreateMap<LoginDTO, UserDTO>().ReverseMap();
        }
    }
}
