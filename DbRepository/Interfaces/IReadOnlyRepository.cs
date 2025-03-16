using DbRepository.Specifications;
using System;
using System.Linq.Expressions;

namespace DbRepository.Interfaces
{
    public interface IReadOnlyRepository<T> where T: class
	{
		public Task<List<T>> GetAllAsync(bool tracked = true);
		public Task<List<T>> GetBySpecificationAsync(ISpecification<T> specification);
		public Task<T> GetAsync(Guid Id, bool tracked = true);
	}
}

