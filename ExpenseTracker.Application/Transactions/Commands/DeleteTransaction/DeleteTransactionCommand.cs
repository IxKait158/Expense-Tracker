using ExpenseTracker.Application.DTOs.Transactions;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest<Unit>;