using System;
namespace FineBudget.Application.Auth
{
    public record AuthResult(
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt,
        UserDto User
    );

    public record UserDto(Guid Id, string Email, string DisplayName);
}

