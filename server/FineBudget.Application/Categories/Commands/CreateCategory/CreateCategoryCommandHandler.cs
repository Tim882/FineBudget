using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Entities;
using FineBudget.Domain.Enums;
using MediatR;

namespace FineBudget.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IAppDbContext _db;

        public CreateCategoryCommandHandler(IAppDbContext db) => _db = db;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var category = new Category(request.Name, request.Icon, (TransactionType)request.DefaultType);
            _db.Categories.Add(category);
            await _db.SaveChangesAsync(ct);
            return category.Id;
        }
    }
}

