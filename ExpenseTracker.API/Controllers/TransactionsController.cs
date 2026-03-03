using ExpenseTracker.Application.Transactions.Commands.CreateTransaction;
using ExpenseTracker.Application.Transactions.Commands.DeleteTransaction;
using ExpenseTracker.Application.Transactions.Commands.UpdateTransaction;
using ExpenseTracker.Application.Transactions.Queries.GetAllTransactions;
using ExpenseTracker.Application.Transactions.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TransactionsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionCommand command)
    {
        var createdTransaction = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = createdTransaction.Id }, createdTransaction);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTransactionCommand command)
    {
        var updatedTransaction = await mediator.Send(command);
        return Ok(updatedTransaction);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await mediator.Send(new GetAllTransactionsQuery());
        return Ok(transactions);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteTransactionCommand(id));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await mediator.Send(new GetTransactionByIdQuery(id));
        return Ok(transaction);
    }
}