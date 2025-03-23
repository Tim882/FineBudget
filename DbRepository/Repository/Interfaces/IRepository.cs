using DbRepository.Specifications;
using System;
namespace Data.Repository.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(TKey id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

