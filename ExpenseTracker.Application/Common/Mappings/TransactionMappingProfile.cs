using AutoMapper;
using ExpenseTracker.Application.DTOs.Transactions;
using ExpenseTracker.Application.Transactions.Commands.CreateTransaction;
using ExpenseTracker.Application.Transactions.Commands.UpdateTransaction;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Common.Mappings;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<CreateTransactionCommand, Transaction>()
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<UpdateTransactionCommand, Transaction>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

        CreateMap<Transaction, TransactionDto>();
    }
}