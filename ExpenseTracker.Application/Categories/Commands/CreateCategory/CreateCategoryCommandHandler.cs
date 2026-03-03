using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var isExistingCategory = await unitOfWork.CategoriesRepository
            .IsExistingUserCategoryNameAsync(request.Name, currentUserService.UserId, cancellationToken);
        if (isExistingCategory)
            throw new ConflictException($"Category {request.Name} already exist.");

        var category = mapper.Map<Category>(request);

        category.UserId = currentUserService.UserId;

        await unitOfWork.CategoriesRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }
}