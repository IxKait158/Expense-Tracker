using ExpenseTracker.Application.DTOs.Categories;
using MediatR;

namespace ExpenseTracker.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<CategoryDto>;