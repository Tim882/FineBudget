using System;
namespace DbRepository.Interfaces
{
	public interface IWriteOnlyRepository<T> where T: class
	{
		public Task<T> CreateAsync(T entity);
		public Task<T> Update(T entity);
		public Task DeleteAsync(Guid Id);
	}
}

