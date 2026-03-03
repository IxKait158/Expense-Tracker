using AutoMapper;
using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    public async Task<IReadOnlyList<CategoryDto>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoriesRepository
            .GetAllUserCategoriesAsync(currentUserService.UserId, cancellationToken);

        return mapper.Map<IReadOnlyList<CategoryDto>>(categories);
    }
}