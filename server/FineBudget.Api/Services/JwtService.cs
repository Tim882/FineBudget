using System;
using FineBudget.Application.Auth;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FineBudget.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IAppDbContext _db;

        public JwtService(IConfiguration configuration, IAppDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<AuthResult> GenerateTokensAsync(User user, CancellationToken ct)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user, ct);

            return new AuthResult(
                accessToken,
                refreshToken,
                DateTime.UtcNow.AddMinutes(15),
                new UserDto(user.Id, user.Email, user.DisplayName)
            );
        }

        private string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.DisplayName)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> GenerateRefreshTokenAsync(User user, CancellationToken ct)
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var token = Convert.ToBase64String(randomBytes);

            var refreshToken = new RefreshToken(
                token,
                user.Id,
                DateTime.UtcNow.AddDays(7)
            );

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync(ct);

            return token;
        }
    }
}

