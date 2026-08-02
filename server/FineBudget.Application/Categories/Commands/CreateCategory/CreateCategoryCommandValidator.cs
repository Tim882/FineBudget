using System;
using FluentValidation;

namespace FineBudget.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название категории обязательно")
                .MaximumLength(50).WithMessage("Название не должно превышать 50 символов");

            RuleFor(x => x.DefaultType)
                .Must(t => t == 1 || t == 2)
                .WithMessage("Тип должен быть 1 (Income) или 2 (Expense)");
        }
    }
}

