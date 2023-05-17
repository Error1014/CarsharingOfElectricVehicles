using AutoMapper;
using Clients.Repository.Entities;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Subscriptions.Repository.Entities;
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
            CreateMap<Client, ClientContactDTO>();
            CreateMap<Client, ClientContactDTO>().ReverseMap();
            CreateMap<Client, ClientDocumentDTO>();
            CreateMap<Client, ClientDocumentDTO>().ReverseMap();
            CreateMap<Passport, PassportDTO>();
            CreateMap<Passport, PassportDTO>().ReverseMap();
            CreateMap<DrivingLicense, DrivingLicenseDTO>();
            CreateMap<DrivingLicense, DrivingLicenseDTO>().ReverseMap();
            CreateMap<ClientSubscription, ClientSubscriptionDTO>();
            CreateMap<ClientSubscription, ClientSubscriptionDTO>().ReverseMap();
            CreateMap<ClientSubscription, SubscribleDTO>();
            CreateMap<ClientSubscription, SubscribleDTO>().ReverseMap();
        }
    }
}
