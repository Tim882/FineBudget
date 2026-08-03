using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IAppDbContext _db;

        public LogoutCommandHandler(IAppDbContext db) => _db = db;

        public async Task Handle(LogoutCommand request, CancellationToken ct)
        {
            var storedToken = await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, ct);

            if (storedToken is not null)
            {
                storedToken.Revoke();
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}

