using ExpenseTracker.Application.DTOs.Categories;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;