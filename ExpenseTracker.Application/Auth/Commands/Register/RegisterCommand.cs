using MediatR;

namespace ExpenseTracker.Application.Auth.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password) : IRequest;