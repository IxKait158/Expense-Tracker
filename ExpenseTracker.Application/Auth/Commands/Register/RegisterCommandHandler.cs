using AutoMapper;
using ExpenseTracker.Application.Common.Exceptions;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.UsersRepository.GetByUsernameAsync(request.Username, cancellationToken) is not null &&
            await unitOfWork.UsersRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
            throw new ConflictException("Username or email already exists.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHasher.GeneratePasswordHash(request.Password)
        };

        await unitOfWork.UsersRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}