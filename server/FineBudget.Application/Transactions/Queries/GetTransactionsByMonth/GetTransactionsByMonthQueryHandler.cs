using System;
using Ardalis.Specification.EntityFrameworkCore;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Application.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Transactions.Queries.GetTransactionsByMonth
{
    public class GetTransactionsByMonthQueryHandler
    : IRequestHandler<GetTransactionsByMonthQuery, List<TransactionDto>>
    {
        private readonly IAppDbContext _db;

        public GetTransactionsByMonthQueryHandler(IAppDbContext db) => _db = db;

        public async Task<List<TransactionDto>> Handle(
            GetTransactionsByMonthQuery request, CancellationToken ct)
        {
            return await _db.Transactions
                .WithSpecification(new TransactionsByMonthSpec(request.Year, request.Month))
                .Select(t => new TransactionDto(
                    t.Id,
                    t.Amount,
                    t.Description,
                    t.Date,
                    t.Type.ToString(),
                    t.CategoryId,
                    t.Category.Name,
                    t.Category.Icon
                ))
                .ToListAsync(ct);
        }
    }
}

