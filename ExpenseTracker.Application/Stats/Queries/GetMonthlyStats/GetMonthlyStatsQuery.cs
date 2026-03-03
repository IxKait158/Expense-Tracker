using MediatR;

namespace ExpenseTracker.Application.Stats.Queries.GetMonthlyStats;

public record GetMonthlyStatsQuery(int Month, int Year) : IRequest<decimal>;