using System;
using Ardalis.Specification;
using FineBudget.Domain.Entities;
using FineBudget.Domain.Enums;

namespace FineBudget.Application.Specifications
{
    public class ExpensesByCategorySpec : Specification<Transaction>
    {
        public ExpensesByCategorySpec(int year, int month)
        {
            Query
                .Where(t => t.Date.Year == year
                         && t.Date.Month == month
                         && t.Type == TransactionType.Expense)
                .Include(t => t.Category);
        }
    }
}

