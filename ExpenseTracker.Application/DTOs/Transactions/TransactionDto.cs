using ExpenseTracker.Application.DTOs.Categories;
using ExpenseTracker.Application.DTOs.Users;

namespace ExpenseTracker.Application.DTOs.Transactions;

public record TransactionDto(
    Guid Id,
    decimal Amount,
    DateTime Date,
    string? Comment,
    CategoryDto Category);