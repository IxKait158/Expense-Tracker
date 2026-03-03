using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext dbContext) : BaseRepository<Transaction>(dbContext), ITransactionRepository
{
    public async Task<Transaction?> GetUserTransactionById(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Transaction?> GetUserTransactionByIdWithDetails(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Transaction>> GetAllUserTransactionsWithDetails(Guid userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .Include(t => t.Category)
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}