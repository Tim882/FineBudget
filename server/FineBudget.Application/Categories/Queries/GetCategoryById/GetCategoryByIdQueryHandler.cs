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
        private readonly ICurrentUserService _currentUser;

        public GetCategoryByIdQueryHandler(IAppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        {
            return await _db.Categories
                .Where(c => c.Id == request.Id && c.UserId == _currentUser.UserId)
                .Select(c => new CategoryDto(c.Id, c.Name, c.Icon, c.DefaultType.ToString()))
                .FirstOrDefaultAsync(ct);
        }
    }
}

