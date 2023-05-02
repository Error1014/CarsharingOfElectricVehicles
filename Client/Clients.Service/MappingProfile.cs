using AutoMapper;
using Clients.Repository.Entities;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientDocumentDTO>();
            CreateMap<Client, ClientDocumentDTO>().ReverseMap();
            CreateMap<Passport, PassportDTO>();
            CreateMap<Passport, PassportDTO>().ReverseMap();
        }
    }
}
