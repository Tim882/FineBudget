using System;
using MediatR;

namespace FineBudget.Application.Auth.Commands.Register
{
    public record RegisterCommand(
        string Email,
        string Password,
        string DisplayName
    ) : IRequest<AuthResult>;
}

