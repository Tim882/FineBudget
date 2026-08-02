using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly IAppDbContext _db;

        public UpdateTransactionCommandHandler(IAppDbContext db) => _db = db;

        public async Task Handle(UpdateTransactionCommand request, CancellationToken ct)
        {
            var transaction = await _db.Transactions
                .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
                ?? throw new KeyNotFoundException($"Транзакция с ID {request.Id} не найдена");

            var categoryExists = await _db.Categories
                .AnyAsync(c => c.Id == request.CategoryId, ct);

            if (!categoryExists)
                throw new InvalidOperationException($"Категория с ID {request.CategoryId} не найдена");

            transaction.Update(
                request.Amount,
                request.Description,
                request.Date,
                (TransactionType)request.Type,
                request.CategoryId
            );

            await _db.SaveChangesAsync(ct);
        }
    }
}

