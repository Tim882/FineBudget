using System;
using MediatR;

namespace FineBudget.Application.Transactions.Commands.CreateTransaction
{
    public record CreateTransactionCommand(
    decimal Amount,
    string Description,
    DateTime Date,
    int Type,
    Guid CategoryId
) : IRequest<Guid>;
}

