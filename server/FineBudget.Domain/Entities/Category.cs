using System;
using FineBudget.Domain.Enums;
using System.Transactions;

namespace FineBudget.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public TransactionType DefaultType { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        private readonly List<Transaction> _transactions = new();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        private Category() { }

        public Category(string name, string icon, TransactionType defaultType, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Icon = icon;
            DefaultType = defaultType;
            UserId = userId;
        }

        public void Update(string name, string icon, TransactionType defaultType)
        {
            Name = name;
            Icon = icon;
            DefaultType = defaultType;
        }
    }
}

