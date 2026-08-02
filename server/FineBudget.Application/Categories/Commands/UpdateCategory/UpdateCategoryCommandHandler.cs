using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IAppDbContext _db;

        public UpdateCategoryCommandHandler(IAppDbContext db) => _db = db;

        public async Task Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, ct)
                ?? throw new KeyNotFoundException($"Категория с ID {request.Id} не найдена");

            category.Update(request.Name, request.Icon, (TransactionType)request.DefaultType);
            await _db.SaveChangesAsync(ct);
        }
    }
}

