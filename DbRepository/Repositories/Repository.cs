using System.Linq;
using System.Linq.Expressions;
using DbRepository.Interfaces;
using DbRepository.Services;
using DbRepository.Specifications;
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
        bool tracked = true)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<List<T>> GetAllAsync(bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
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

    public async Task<List<T>> GetBySpecificationAsync(ISpecification<T> specification)
    {
        var query = ApplySpecification(specification);
        return await query.ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = _context.Set<T>().AsQueryable();

        // Apply filtering
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Apply includes
        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        // Apply sorting
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // Apply pagination
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }
}

