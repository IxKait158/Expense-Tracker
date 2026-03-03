using ExpenseTracker.Application.DTOs.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto>;