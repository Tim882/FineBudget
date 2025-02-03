using System;
using DbRepository;
using DbRepository.Interfaces;
using Models.DbModels.MainModels;

namespace FineBudget.UnitOfWork
{
	public class UnitOfWork: IUnitOfWork
	{
        private bool _disposed;
        private readonly BudgetContext _context;

        private IRepository<Account> _accountRepository;
        private IRepository<Asset> _assetRepository;
        private IRepository<Cost> _costRepository;
        private IRepository<Income> _incomeRepository;
        private IRepository<Liability> _liabilityRepository;

        public IRepository<Account> AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new Repository<Account>(_context);

                return _accountRepository;
            }
        }
        public IRepository<Asset> AssetRepository
        {
            get
            {
                if (_assetRepository == null)
                    _assetRepository = new Repository<Asset>(_context);

                return _assetRepository;
            }
        }
        public IRepository<Cost> CostRepository
        {
            get
            {
                if (_costRepository == null)
                    _costRepository = new Repository<Cost>(_context);

                return _costRepository;
            }
        }
        public IRepository<Income> IncomeRepository
        {
            get
            {
                if (_incomeRepository == null)
                    _incomeRepository = new Repository<Income>(_context);

                return _incomeRepository;
            }
        }
        public IRepository<Liability> LiabilityRepository
        {
            get
            {
                if (_liabilityRepository == null)
                    _liabilityRepository = new Repository<Liability>(_context);

                return _liabilityRepository;
            }
        }

        public UnitOfWork(BudgetContext context)
		{
            _context = context;
		}

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}

