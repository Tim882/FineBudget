using System;
namespace FineBudget.Application.Transactions.Queries.GetTransactionsByMonth
{
    public record TransactionDto(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    string Type,
    Guid CategoryId,
    string CategoryName,
    string CategoryIcon
);
}

