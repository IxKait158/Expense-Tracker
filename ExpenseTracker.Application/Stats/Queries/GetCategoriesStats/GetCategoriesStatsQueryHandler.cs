using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Stats.Queries.GetCategoriesStats;

public class GetCategoriesStatsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetCategoriesStatsQuery, List<CategoryStatDto>>
{
    public async Task<List<CategoryStatDto>> Handle(GetCategoriesStatsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.StatsRepository.GetTotalExpenseAmountForCategories(currentUserService.UserId, cancellationToken);
    }
}