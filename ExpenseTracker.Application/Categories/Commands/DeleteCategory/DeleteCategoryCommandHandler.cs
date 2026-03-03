using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.Interfaces;
using MediatR;
using InvalidOperationException = System.InvalidOperationException;

namespace ExpenseTracker.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoriesRepository
            .GetUserCategoryByIdAsync(request.Id, currentUserService.UserId, cancellationToken);
        if (category is null)
            throw new NotFoundException(nameof(category), request.Id);

        var hasTransactions = await unitOfWork.CategoriesRepository.HasTransactionsAsync(request.Id, currentUserService.UserId, cancellationToken);
        if (hasTransactions)
            throw new ConflictException(
                "Cannot delete category because it has associated transactions. Please delete or reassign all transactions first.");


        await unitOfWork.CategoriesRepository.DeleteAsync(request.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}