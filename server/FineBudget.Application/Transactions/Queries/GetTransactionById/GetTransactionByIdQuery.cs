using System;
using FineBudget.Application.Transactions.Queries.GetTransactionsByMonth;
using MediatR;

namespace FineBudget.Application.Transactions.Queries.GetTransactionById
{
    public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto?>;
}

