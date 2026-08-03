using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly IAppDbContext _db;
        private readonly ICurrentUserService _currentUser;

        public GetCategoriesQueryHandler(IAppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            return await _db.Categories
                .Where(c => c.UserId == _currentUser.UserId)
                .OrderBy(c => c.Name)
                .Select(c => new CategoryDto(c.Id, c.Name, c.Icon, c.DefaultType.ToString()))
                .ToListAsync(ct);
        }
    }
}

