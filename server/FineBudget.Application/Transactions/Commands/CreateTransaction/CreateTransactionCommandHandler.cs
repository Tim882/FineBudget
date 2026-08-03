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
        private readonly ICurrentUserService _currentUser;

        public CreateTransactionCommandHandler(IAppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken ct)
        {
            var categoryExists = await _db.Categories
                .AnyAsync(c => c.Id == request.CategoryId, ct);

            if (!categoryExists)
                throw new InvalidOperationException($"Категория с ID {request.CategoryId} не найдена");

            var dateUtc = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);

            var transaction = new Transaction(
                request.Amount,
                request.Description,
                dateUtc,
                (TransactionType)request.Type,
                request.CategoryId,
                _currentUser.UserId
            );

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync(ct);
            return transaction.Id;
        }
    }
}

