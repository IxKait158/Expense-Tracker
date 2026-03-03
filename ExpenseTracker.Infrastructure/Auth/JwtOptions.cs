namespace ExpenseTracker.Infrastructure.Auth;

public class JwtOptions()
{
    public string SecretKey { get; set; } = null!;
    public int ExpiresHours { get; set; }
}