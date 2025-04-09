using FineBudget.Models;

namespace FineBudget.Models
{
    public class Cost : Operation
    {
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

