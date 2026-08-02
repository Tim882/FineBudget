using System;
using FineBudget.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly IAppDbContext _db;

        public DeleteTransactionCommandHandler(IAppDbContext db) => _db = db;

        public async Task Handle(DeleteTransactionCommand request, CancellationToken ct)
        {
            var transaction = await _db.Transactions
                .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
                ?? throw new KeyNotFoundException($"Транзакция с ID {request.Id} не найдена");

            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync(ct);
        }
    }
}

