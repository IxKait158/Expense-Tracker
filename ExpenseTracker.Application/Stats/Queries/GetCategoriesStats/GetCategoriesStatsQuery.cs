using ExpenseTracker.Application.DTOs.Categories;
using MediatR;

namespace ExpenseTracker.Application.Stats.Queries.GetCategoriesStats;

public record GetCategoriesStatsQuery() : IRequest<List<CategoryStatDto>>;