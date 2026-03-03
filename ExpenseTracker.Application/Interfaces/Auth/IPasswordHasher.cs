namespace ExpenseTracker.Application.Interfaces.Auth;

public interface IPasswordHasher
{
    string GeneratePasswordHash(string password);

    bool VerifyPasswordHash(string password, string hashedPassword);
}