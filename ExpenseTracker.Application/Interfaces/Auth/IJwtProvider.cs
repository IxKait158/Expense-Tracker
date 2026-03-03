using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}