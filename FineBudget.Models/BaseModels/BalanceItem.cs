using Base.Models;

namespace FineBudget.Models
{
    public abstract class BalanceItem : BaseEntity
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public List<Income> Incomes { get; set; }
        public List<Cost> Costs { get; set; }
    }
}

