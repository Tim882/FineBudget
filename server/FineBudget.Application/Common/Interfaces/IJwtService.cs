using System;
using FineBudget.Application.Auth;
using FineBudget.Domain.Entities;

namespace FineBudget.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<AuthResult> GenerateTokensAsync(User user, CancellationToken ct);
    }
}

