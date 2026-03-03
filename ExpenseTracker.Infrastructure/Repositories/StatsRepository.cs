using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories;

public class StatsRepository(AppDbContext dbContext) : IStatsRepository
{
    public async Task<decimal> GetTotalExpenseAmountForMonth(Guid userId, int month, int year, CancellationToken cancellationToken)
    {
        return await dbContext
            .Transactions
            .AsNoTracking()
            .Include(t => t.User)
            .Where(t => t.UserId == userId && t.Date.Month == month && t.Date.Year == year)
            .Select(t => t.Amount)
            .SumAsync(cancellationToken);
    }

    public async Task<List<CategoryStatDto>> GetTotalExpenseAmountForCategories(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext
            .Transactions
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .GroupBy(t => t.Category.Name)
            .Select(g => new CategoryStatDto
            (
                g.Key,
                g.Sum(t => t.Amount)
            ))
            .ToListAsync(cancellationToken);
    }
}