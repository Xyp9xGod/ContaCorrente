using AutoMapper;
using ContaCorrente.Application.DTOs;
using ContaCorrente.Domain.Entities;

namespace ContaCorrente.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            //CreateMap<BankAccount, DepositDTO>().ReverseMap();
        }
    }
}
