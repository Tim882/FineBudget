using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IAppDbContext _db;
        private readonly IJwtService _jwtService;

        public RegisterCommandHandler(IAppDbContext db, IJwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken ct)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email, ct))
                throw new InvalidOperationException("Пользователь с таким email уже существует");

            var user = new User(
                request.Email,
                BCrypt.Net.BCrypt.HashPassword(request.Password),
                request.DisplayName
            );

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);

            return await _jwtService.GenerateTokensAsync(user, ct);
        }
    }
}

