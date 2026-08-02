using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IAppDbContext _db;

        public DeleteCategoryCommandHandler(IAppDbContext db) => _db = db;

        public async Task Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await _db.Categories
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == request.Id, ct)
                ?? throw new KeyNotFoundException($"Категория с ID {request.Id} не найдена");

            if (category.Transactions.Any())
                throw new InvalidOperationException(
                    "Нельзя удалить категорию, к которой привязаны транзакции. " +
                    "Сначала удалите или переместите транзакции.");

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync(ct);
        }
    }
}

