using System.Security.Claims;
using ExpenseTracker.Application.Interfaces;

namespace ExpenseTracker.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            return idClaim is null ? throw new UnauthorizedAccessException() : Guid.Parse(idClaim.Value);
        }
    }

    public string Username
    {
        get
        {
            var usernameClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
            return usernameClaim is null ? throw new UnauthorizedAccessException() : usernameClaim.Value;
        }
    }
}