namespace FineBudget.Services.Interfaces
{
    public interface IImportDataService
    {
        public Task<bool> ImportFromMoneyFlowCsv();
    }
}
