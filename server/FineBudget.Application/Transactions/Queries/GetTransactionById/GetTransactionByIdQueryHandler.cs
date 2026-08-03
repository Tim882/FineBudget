using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Application.Transactions.Queries.GetTransactionsByMonth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler
    : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
    {
        private readonly IAppDbContext _db;
        private readonly ICurrentUserService _currentUser;

        public GetTransactionByIdQueryHandler(IAppDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken ct)
        {
            return await _db.Transactions
                .Where(t => t.Id == request.Id && t.UserId == _currentUser.UserId)
                .Select(t => new TransactionDto(
                    t.Id,
                    t.Amount,
                    t.Description,
                    t.Date,
                    t.Type.ToString(),
                    t.CategoryId,
                    t.Category.Name,
                    t.Category.Icon
                ))
                .FirstOrDefaultAsync(ct);
        }
    }
}

