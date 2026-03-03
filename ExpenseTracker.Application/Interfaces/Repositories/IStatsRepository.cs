using ExpenseTracker.Application.DTOs.Categories;

namespace ExpenseTracker.Application.Interfaces.Repositories;

public interface IStatsRepository
{
    Task<decimal> GetTotalExpenseAmountForMonth(Guid userId, int month, int year, CancellationToken cancellationToken);

    Task<List<CategoryStatDto>> GetTotalExpenseAmountForCategories(Guid userId, CancellationToken cancellationToken);
}