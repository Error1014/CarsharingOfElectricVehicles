using AutoMapper;
using Configuration.Repository.Entities;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigurationItem, ConfigurationItemDTO>();
            CreateMap<ConfigurationItem, ConfigurationItemDTO>().ReverseMap();
        }
    }
}
