using System;
using MediatR;

namespace FineBudget.Application.Auth.Commands.Logout
{
    public record LogoutCommand(string RefreshToken) : IRequest;
}

