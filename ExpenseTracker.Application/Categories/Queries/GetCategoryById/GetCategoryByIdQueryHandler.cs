using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoriesRepository
            .GetUserCategoryByIdAsync(request.Id, currentUserService.UserId ,cancellationToken);
        if (category is null)
            throw new NotFoundException(nameof(category), request.Id);

        return mapper.Map<CategoryDto>(category);
    }
}