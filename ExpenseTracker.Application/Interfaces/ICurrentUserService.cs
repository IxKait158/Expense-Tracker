namespace ExpenseTracker.Application.Interfaces;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    public string Username { get; }
}