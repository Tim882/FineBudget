using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
