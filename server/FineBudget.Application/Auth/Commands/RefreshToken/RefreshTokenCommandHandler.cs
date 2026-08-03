using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        private readonly IAppDbContext _db;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(IAppDbContext db, IJwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var storedToken = await _db.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, ct);

            if (storedToken is null || !storedToken.IsValid())
                throw new UnauthorizedAccessException("Недействительный refresh-токен");

            // Отзываем старый токен
            storedToken.Revoke();

            // Генерируем новую пару
            return await _jwtService.GenerateTokensAsync(storedToken.User, ct);
        }
    }
}

