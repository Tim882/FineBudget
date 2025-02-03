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

        public virtual async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task DeleteAsync(long Id)
        {
            T entity = await _context.Set<T>().FindAsync(Id);

            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

