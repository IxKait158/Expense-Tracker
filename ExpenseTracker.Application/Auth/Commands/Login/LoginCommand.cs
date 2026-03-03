using ExpenseTracker.Application.DTOs.Users;
using MediatR;

namespace ExpenseTracker.Application.Auth.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<string>;