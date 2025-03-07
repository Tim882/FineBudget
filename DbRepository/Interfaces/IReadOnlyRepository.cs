using System;
using System.Linq.Expressions;

namespace DbRepository.Interfaces
{
	public interface IReadOnlyRepository<T> where T: class
	{
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, bool tracked = true, string includeProperties = null);
		public Task<T> GetAsync(Guid Id, bool tracked = true, string includeProperties = null);
	}
}

