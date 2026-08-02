using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly IAppDbContext _db;

        public GetCategoriesQueryHandler(IAppDbContext db) => _db = db;

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            return await _db.Categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryDto(c.Id, c.Name, c.Icon, c.DefaultType.ToString()))
                .ToListAsync(ct);
        }
    }
}

