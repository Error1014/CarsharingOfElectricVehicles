using AutoMapper;
using Infrastructure.DTO;
using TransactionsSystem.Repository.Entities;

namespace TransactionsSystem.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionItem, TransactionItemDTO>();
            CreateMap<TransactionItem, TransactionItemDTO>().ReverseMap();
            CreateMap<TransactionItem, TransactionAddDTO>();
            CreateMap<TransactionItem, TransactionAddDTO>().ReverseMap();
        }
    }
}
