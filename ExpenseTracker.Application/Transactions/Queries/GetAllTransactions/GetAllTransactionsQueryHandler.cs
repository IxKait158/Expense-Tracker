using AutoMapper;
using ExpenseTracker.Application.DTOs.Transactions;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Queries.GetAllTransactions;

public class GetAllTransactionsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetAllTransactionsQuery, IReadOnlyList<TransactionDto>>
{
    public async Task<IReadOnlyList<TransactionDto>> Handle(
        GetAllTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.TransactionsRepository
            .GetAllUserTransactionsWithDetails(currentUserService.UserId, cancellationToken);

        return mapper.Map<IReadOnlyList<TransactionDto>>(transactions);
    }
}