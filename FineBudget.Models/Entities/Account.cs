using Base.Models;

namespace FineBudget.Models
{
    public class Account : BaseEntity
    {
        public string Title { get; set; }
        public decimal Balance { get; set; }

        public List<Income> Incomes { get; set; }
        public List<Cost> Costs { get; set; }
    }
}

