using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category?> GetUserCategoryByNameAsync(string name, Guid userId, CancellationToken cancellationToken);

    Task<Category?> GetUserCategoryByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);

    Task<List<Category>> GetAllUserCategoriesAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsExistingUserCategoryNameAsync(string name, Guid userId, CancellationToken cancellationToken);

    Task<bool> HasTransactionsAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}