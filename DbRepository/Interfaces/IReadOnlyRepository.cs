using System;
using System.Linq.Expressions;

namespace DbRepository.Interfaces
{
	public interface IReadOnlyRepository<T> where T: class
	{
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
		public Task<T> GetAsync(Guid Id);
	}
}

