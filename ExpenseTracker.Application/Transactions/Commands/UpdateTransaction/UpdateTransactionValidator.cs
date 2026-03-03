using FluentValidation;

namespace ExpenseTracker.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Transaction ID is required.");

        RuleFor(x => x.Date)
            .Must(x => x <= DateTime.UtcNow).WithMessage("Date cant be in the future.")
            .When(x => x.Date.HasValue);
    }
}