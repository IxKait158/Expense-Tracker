using ExpenseTracker.Application.DTOs.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(
    Guid Id,
    decimal? Amount,
    DateTime? Date,
    string? Comment)
    : IRequest<TransactionDto>;