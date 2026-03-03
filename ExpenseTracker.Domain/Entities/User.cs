namespace ExpenseTracker.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public List<Category>? Categories { get; set; } = [];

    public List<Transaction>? Transactions { get; set; } = [];
}