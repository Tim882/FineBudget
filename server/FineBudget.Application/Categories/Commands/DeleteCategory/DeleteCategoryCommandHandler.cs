using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IAppDbContext _db;
        private readonly ICurrentUserService _currentUser;

        public DeleteCategoryCommandHandler(IAppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await _db.Categories
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == _currentUser.UserId, ct)
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

