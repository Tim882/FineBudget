using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Entities;
using FineBudget.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IAppDbContext _db;

        public CreateTransactionCommandHandler(IAppDbContext db) => _db = db;

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken ct)
        {
            var categoryExists = await _db.Categories
                .AnyAsync(c => c.Id == request.CategoryId, ct);

            if (!categoryExists)
                throw new InvalidOperationException($"Категория с ID {request.CategoryId} не найдена");

            var transaction = new Transaction(
                request.Amount,
                request.Description,
                request.Date,
                (TransactionType)request.Type,
                request.CategoryId
            );

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync(ct);
            return transaction.Id;
        }
    }
}

