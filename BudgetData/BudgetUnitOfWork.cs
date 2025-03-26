using Data.UnitOfWork;
using FineBudget;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetData
{
    public class BudgetUnitOfWork : UnitOfWork<BudgetContext>, IUnitOfWork
    {
        public BudgetUnitOfWork(BudgetContext context) : base(context)
        {
        }
    }
}
