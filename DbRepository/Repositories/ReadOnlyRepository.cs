using System;
using System.Linq.Expressions;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbRepository.Services
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbSet;

        public ReadOnlyRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> GetAsync(Guid Id, 
            bool tracked = true, 
            string includeProperties = null)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter, 
            bool tracked = true, 
            string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return await query.ToListAsync();
        }
    }
}

