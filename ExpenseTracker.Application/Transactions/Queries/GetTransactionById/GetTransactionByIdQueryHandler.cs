using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Transactions;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    public async Task<TransactionDto> Handle(
        GetTransactionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.TransactionsRepository
            .GetUserTransactionByIdWithDetails(request.Id, currentUserService.UserId, cancellationToken);
        if (transaction is null)
            throw new NotFoundException(nameof(transaction), request.Id);

        return mapper.Map<TransactionDto>(transaction);
    }
}