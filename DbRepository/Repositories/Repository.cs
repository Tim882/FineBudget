using System.Linq.Expressions;
using DbRepository.Interfaces;
using DbRepository.Services;
using Microsoft.EntityFrameworkCore;

namespace DbRepository;

public class Repository<T> : IRepository<T>  where T : class
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        var result = await _context.Set<T>().AddAsync(entity);

        return result.Entity;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        T entity = await _context.Set<T>().FindAsync(id);

        if (entity != null)
            _context.Set<T>().Remove(entity);
    }

    public virtual Task<T> Update(T entity)
    {
        return Task.FromResult(_context.Set<T>().Update(entity).Entity);
    }
}

