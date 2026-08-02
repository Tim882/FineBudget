using System;
using MediatR;

namespace FineBudget.Application.Transactions.Queries.GetTransactionsByMonth
{
    public record GetTransactionsByMonthQuery(int Year, int Month) : IRequest<List<TransactionDto>>;
}

