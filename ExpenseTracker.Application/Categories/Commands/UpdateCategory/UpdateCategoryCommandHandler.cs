using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle
        (UpdateCategoryCommand request,
            CancellationToken cancellationToken)
    {
        var isExistingCategory = await unitOfWork.CategoriesRepository
            .IsExistingUserCategoryNameAsync(request.Name, currentUserService.UserId, cancellationToken);
        if (isExistingCategory)
            throw new ConflictException($"Category {request.Name} already exist.");

        var category = await unitOfWork.CategoriesRepository
            .GetUserCategoryByIdAsync(request.Id, currentUserService.UserId, cancellationToken);
        if (category is null)
            throw new NotFoundException(nameof(category), request.Id);

        mapper.Map(request, category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }
}