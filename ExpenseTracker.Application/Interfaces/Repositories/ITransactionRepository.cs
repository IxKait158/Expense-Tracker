using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Repositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    Task<Transaction?> GetUserTransactionById(Guid id, Guid userId, CancellationToken cancellationToken);

    Task<Transaction?> GetUserTransactionByIdWithDetails(Guid id, Guid userId, CancellationToken cancellationToken);

    Task<List<Transaction>> GetAllUserTransactionsWithDetails(Guid userId, CancellationToken cancellationToken);
}