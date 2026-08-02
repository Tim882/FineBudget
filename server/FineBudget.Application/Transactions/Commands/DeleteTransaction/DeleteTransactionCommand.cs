using System;
using MediatR;

namespace FineBudget.Application.Transactions.Commands.DeleteTransaction
{
    public record DeleteTransactionCommand(Guid Id) : IRequest;
}

