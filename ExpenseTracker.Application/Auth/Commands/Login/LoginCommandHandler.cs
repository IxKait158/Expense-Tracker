using AutoMapper;
using ExpenseTracker.Application.DTOs.Users;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Auth;
using MediatR;

namespace ExpenseTracker.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider,
    IPasswordHasher passwordHasher,
    IMapper mapper)
    : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetByUsernameAsync(request.Username, cancellationToken);

        bool isPasswordValid = user != null && passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash);

        if (user is null || !isPasswordValid)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = jwtProvider.GenerateToken(user);
        return token;
    }
}