using System;
using MediatR;

namespace FineBudget.Application.Transactions.Commands.UpdateTransaction
{
    public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    int Type,
    Guid CategoryId
) : IRequest;
}

