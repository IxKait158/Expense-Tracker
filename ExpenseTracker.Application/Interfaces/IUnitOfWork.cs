using ExpenseTracker.Application.Interfaces.Repositories;

namespace ExpenseTracker.Application.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository CategoriesRepository { get; }
    ITransactionRepository TransactionsRepository { get; }
    IUserRepository UsersRepository { get; }

    IStatsRepository StatsRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}