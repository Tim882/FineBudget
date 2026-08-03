using System;
namespace FineBudget.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string DisplayName { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<Transaction> _transactions = new();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        private readonly List<Category> _categories = new();
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private User() { }

        public User(string email, string passwordHash, string displayName)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            DisplayName = displayName;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

