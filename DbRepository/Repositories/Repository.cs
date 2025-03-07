using System.Linq.Expressions;
using DbRepository.Interfaces;
using DbRepository.Services;
using Microsoft.EntityFrameworkCore;

namespace DbRepository;

public class Repository<T> : IRepository<T>  where T : class
{
    private readonly DbContext _context;
    private DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<T> GetAsync(
        Guid id,
        bool tracked = true,
        string includeProperties = null)
    {
        return await _context.Set<T>().FindAsync(id);
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

    public virtual async Task<T> Update(T entity)
    {
        return await Task.FromResult(_context.Set<T>().Update(entity).Entity);
    }
}

