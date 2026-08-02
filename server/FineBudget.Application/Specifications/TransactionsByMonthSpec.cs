using System;
using Ardalis.Specification;
using FineBudget.Domain.Entities;

namespace FineBudget.Application.Specifications
{
    public class TransactionsByMonthSpec : Specification<Transaction>
    {
        public TransactionsByMonthSpec(int year, int month)
        {
            Query
                .Where(t => t.Date.Year == year && t.Date.Month == month)
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date);
        }
    }
}

