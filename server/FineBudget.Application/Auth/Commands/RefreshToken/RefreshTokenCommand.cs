using System;
using MediatR;

namespace FineBudget.Application.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<AuthResult>;
}

