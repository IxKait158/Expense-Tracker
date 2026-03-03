using ExpenseTracker.Application.DTOs.Categories;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;