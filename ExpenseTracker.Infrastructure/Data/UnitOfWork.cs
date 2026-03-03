using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Repositories;

namespace ExpenseTracker.Infrastructure.Data;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public ICategoryRepository CategoriesRepository { get; } = new CategoryRepository(dbContext);
    public ITransactionRepository TransactionsRepository { get; } = new TransactionRepository(dbContext);
    public IUserRepository UsersRepository { get; } = new UserRepository(dbContext);

    public IStatsRepository StatsRepository { get; } = new StatsRepository(dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}