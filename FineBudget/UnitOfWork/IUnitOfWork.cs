using System;
using DbRepository.Interfaces;
using Models.DbModels;
using Models.DbModels.BaseModels;
using Models.DbModels.MainModels;

namespace FineBudget.UnitOfWork
{
	public interface IUnitOfWork: IDisposable
	{
		IRepository<Account> AccountRepository { get; }
        IRepository<Asset> AssetRepository { get; }
        IRepository<Cost> CostRepository { get; }
        IRepository<Income> IncomeRepository { get; }
        IRepository<Liability> LiabilityRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();

        Task<int> SaveAsync();
    }
}

