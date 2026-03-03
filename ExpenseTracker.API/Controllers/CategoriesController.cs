using ExpenseTracker.Application.Categories.Commands.CreateCategory;
using ExpenseTracker.Application.Categories.Commands.DeleteCategory;
using ExpenseTracker.Application.Categories.Commands.UpdateCategory;
using ExpenseTracker.Application.Categories.Queries.GetAllCategories;
using ExpenseTracker.Application.Categories.Queries.GetCategoryById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var createdCategory = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryCommand command)
    {
        var updatedCategory = await mediator.Send(command);
        return Ok(updatedCategory);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(category);
    }
}