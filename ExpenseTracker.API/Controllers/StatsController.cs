using ExpenseTracker.Application.Stats.Queries.GetCategoriesStats;
using ExpenseTracker.Application.Stats.Queries.GetMonthlyStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StatsController(IMediator mediator) : ControllerBase
{
    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthly([FromQuery] GetMonthlyStatsQuery query)
    {
        var sum = await mediator.Send(query);
        return Ok(sum);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categoriesStats = await mediator.Send(new GetCategoriesStatsQuery());
        return Ok(categoriesStats);
    }
}