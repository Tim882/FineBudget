using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly IAppDbContext _db;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IAppDbContext db, IJwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, ct);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Неверный email или пароль");

            return await _jwtService.GenerateTokensAsync(user, ct);
        }
    }
}

