using CsvHelper.Configuration.Attributes;

namespace FineBudget.Models
{
    public class MoneyFlowTransactionItem
    {
        [Index(0)]
        public DateTime TransactionDate { get; set; }
        [Index(1)]
        public decimal Sum { get; set; }
        [Index(2)]
        public string Currency { get; set; }
        [Index(3)]
        public string Account { get; set; }
        [Index(4)]
        public string TransactionAccount { get; set; }
        [Index(5)]
        public string Category { get; set; }
        [Index(6)]
        public string Note { get; set; }
        [Index(7)]
        public string Contragent { get; set; }
        [Index(8)]
        public string Place { get; set; }
    }
}
