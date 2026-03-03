using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteTransactionCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.TransactionsRepository
            .GetUserTransactionById(request.Id, currentUserService.UserId, cancellationToken);
        if (transaction is null)
            throw new NotFoundException(nameof(transaction), request.Id);

        await unitOfWork.TransactionsRepository.DeleteAsync(transaction.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}