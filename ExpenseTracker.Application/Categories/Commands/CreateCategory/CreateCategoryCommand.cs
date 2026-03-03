using ExpenseTracker.Application.DTOs.Categories;
using MediatR;

namespace ExpenseTracker.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;