using System;
using FluentValidation;

namespace FineBudget.Application.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен")
                .MinimumLength(6).WithMessage("Пароль должен быть не менее 6 символов");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100);
        }
    }
}

