using AutoMapper;
using Infrastructure.DTO;
using Infrastructure.DTO.Rent;
using Rents.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tariff, TariffDTO>();
            CreateMap<Tariff, TariffDTO>().ReverseMap();
            CreateMap<Rent, RentDTO>();
            CreateMap<Rent, RentDTO>().ReverseMap();
            CreateMap<Rent, AddRentDTO>();
            CreateMap<Rent, AddRentDTO>().ReverseMap();
        }
    }
}
