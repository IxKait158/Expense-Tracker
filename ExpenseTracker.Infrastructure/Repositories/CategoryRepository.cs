using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<Category?> GetUserCategoryByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .FirstOrDefaultAsync(c => string.Equals(c.Name, name), cancellationToken);
    }

    public async Task<Category?> GetUserCategoryByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(c => c.UserId == userId)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<Category>> GetAllUserCategoriesAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsExistingUserCategoryNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> HasTransactionsAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Transactions
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .AnyAsync(t => t.CategoryId == id, cancellationToken);
    }
}