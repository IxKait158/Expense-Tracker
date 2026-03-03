using ExpenseTracker.Application.DTOs.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Queries.GetAllTransactions;

public record GetAllTransactionsQuery() : IRequest<IReadOnlyList<TransactionDto>>;