namespace FineBudget.Models
{
    public class MoneyFlowTransactionItem
    {
        public DateTime TransactionDate { get; set; }
        public decimal Sum { get; set; }
        public string Account { get; set; }
        public string TransactionAccount { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
        public string Contragent { get; set; }
        public string Place { get; set; }
    }
}
