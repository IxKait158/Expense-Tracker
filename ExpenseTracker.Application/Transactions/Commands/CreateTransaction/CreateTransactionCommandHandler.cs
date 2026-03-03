using System.Security.Authentication;
using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Transactions;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoriesRepository
            .GetUserCategoryByIdAsync(request.CategoryId, currentUserService.UserId, cancellationToken);
        if (category is null)
            throw new NotFoundException(nameof(category), request.CategoryId);

        var transaction = mapper.Map<Transaction>(request);
        transaction.CategoryId = category.Id;
        transaction.UserId = currentUserService.UserId;

        await unitOfWork.TransactionsRepository.AddAsync(transaction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<TransactionDto>(transaction);
    }
}