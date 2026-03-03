using ExpenseTracker.Application.DTOs.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    decimal Amount,
    DateTime? Date,
    string? Comment,
    Guid CategoryId)
    : IRequest<TransactionDto>;