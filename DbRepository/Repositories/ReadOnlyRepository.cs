using System;
using System.Linq.Expressions;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbRepository.Services
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        private readonly DbContext _context;

        public ReadOnlyRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetAsync(long Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}

