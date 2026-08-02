using System;
using FluentValidation;

namespace FineBudget.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма должна быть больше нуля");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно")
                .MaximumLength(200).WithMessage("Описание не должно превышать 200 символов");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория обязательна");

            RuleFor(x => x.Type)
                .Must(t => t == 1 || t == 2)
                .WithMessage("Тип должен быть 1 (Income) или 2 (Expense)");
        }
    }
}

