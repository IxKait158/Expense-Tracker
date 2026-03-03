using ExpenseTracker.Application.Interfaces;
using MediatR;

namespace ExpenseTracker.Application.Stats.Queries.GetMonthlyStats;

public class GetMonthlyStatsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetMonthlyStatsQuery, decimal>
{
    public async Task<decimal> Handle(GetMonthlyStatsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.StatsRepository.GetTotalExpenseAmountForMonth(currentUserService.UserId, request.Month, request.Year, cancellationToken);
    }
}