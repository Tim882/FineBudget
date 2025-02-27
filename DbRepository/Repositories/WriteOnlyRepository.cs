using System;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbRepository.Services
{
    public class WriteOnlyRepository<T> : IWriteOnlyRepository<T> where T : class
    {
        private readonly DbContext _context;

        public WriteOnlyRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);

            return result.Entity;
        }

        public virtual async Task DeleteAsync(Guid Id)
        {
            T entity = await _context.Set<T>().FindAsync(Id);

            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public virtual async Task<T> Update(T entity)
        {
            return await Task.FromResult(_context.Set<T>().Update(entity).Entity);
        }
    }
}

