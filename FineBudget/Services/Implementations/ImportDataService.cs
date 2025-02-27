using FileHandler;
using FineBudget.Models;
using FineBudget.Services.Interfaces;

namespace FineBudget.Services.Implementations
{
    public class ImportDataService : IImportDataService
    {
        private readonly IFileReader _fileReader;

        public ImportDataService(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public async Task<bool> ImportFromMoneyFlowCsv()
        {
            string fileNameWithPath = "C:\\Users\\1\\Downloads\\Telegram Desktop\\Money-Flow-Transactions.csv";

            var transactions = await _fileReader.ReadFileAsync<MoneyFlowTransactionItem>(fileNameWithPath);

            return true;
        }
    }
}
