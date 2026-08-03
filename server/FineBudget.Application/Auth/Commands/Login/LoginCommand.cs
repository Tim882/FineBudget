using System;
using MediatR;

namespace FineBudget.Application.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResult>;
}

