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

    public virtual async Task<T> GetAsync(long Id)
    {
        return await _context.Set<T>().FindAsync(Id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
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

