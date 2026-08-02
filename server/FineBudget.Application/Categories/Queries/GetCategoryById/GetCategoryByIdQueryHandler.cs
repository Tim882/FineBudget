using System;
using FineBudget.Application.Categories.Queries.GetCategories;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly IAppDbContext _db;

        public GetCategoryByIdQueryHandler(IAppDbContext db) => _db = db;

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        {
            return await _db.Categories
                .Where(c => c.Id == request.Id)
                .Select(c => new CategoryDto(c.Id, c.Name, c.Icon, c.DefaultType.ToString()))
                .FirstOrDefaultAsync(ct);
        }
    }
}

