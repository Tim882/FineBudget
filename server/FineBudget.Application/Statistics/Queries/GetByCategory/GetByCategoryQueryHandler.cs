using System;
using Ardalis.Specification.EntityFrameworkCore;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Application.Specifications;
using FineBudget.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Statistics.Queries.GetByCategory
{
    public class GetByCategoryQueryHandler
    : IRequestHandler<GetByCategoryQuery, List<CategoryStatDto>>
    {
        private readonly IAppDbContext _db;

        public GetByCategoryQueryHandler(IAppDbContext db) => _db = db;

        public async Task<List<CategoryStatDto>> Handle(
            GetByCategoryQuery request, CancellationToken ct)
        {
            var grouped = await _db.Transactions
                .Where(t => t.Date.Year == request.Year
                         && t.Date.Month == request.Month
                         && t.Type == TransactionType.Expense)
                .GroupBy(t => new { t.CategoryId, t.Category.Name, t.Category.Icon })
                .Select(g => new
                {
                    g.Key.Name,
                    g.Key.Icon,
                    Total = g.Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync(ct);

            return grouped
                .Select(x => new CategoryStatDto(x.Name, x.Icon, x.Total))
                .ToList();
        }
    }
}

