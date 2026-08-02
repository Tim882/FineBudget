using System;
using FineBudget.Domain.Enums;

namespace FineBudget.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType Type { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Transaction() { }

        public Transaction(decimal amount, string description, DateTime date,
                           TransactionType type, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Description = description;
            Date = date;
            Type = type;
            CategoryId = categoryId;
        }

        public void Update(decimal amount, string description, DateTime date,
                       TransactionType type, Guid categoryId)
        {
            Amount = amount;
            Description = description;
            Date = date;
            Type = type;
            CategoryId = categoryId;
        }
    }
}

