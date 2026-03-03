using FluentValidation;

namespace ExpenseTracker.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Date)
            .NotNull().WithMessage("Date is required.")
            .Must(x => x <= DateTime.UtcNow).WithMessage("Date cant be in the future.")
            .When(x => x.Date.HasValue);

        RuleFor(x => x.Comment)
            .NotNull().WithMessage("Comment is required.")
            .When(x => x.Comment != null);

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
    }
}