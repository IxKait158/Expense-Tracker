using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Transactions;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.TransactionsRepository
            .GetUserTransactionById(request.Id, currentUserService.UserId, cancellationToken);
        if (transaction is null)
            throw new NotFoundException(nameof(transaction), request.Id);

        mapper.Map(request, transaction);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<TransactionDto>(transaction);
    }
}