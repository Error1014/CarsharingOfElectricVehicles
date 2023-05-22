using AutoMapper;
using Cars.Repository.Entities;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDTO>();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<BrandModel, BrandModelDTO>();
            CreateMap<BrandModel, BrandModelDTO>().ReverseMap();
            CreateMap<Characteristic, CharacteristicDTO>();
            CreateMap<Characteristic, CharacteristicDTO>().ReverseMap();
        }
    }
}
