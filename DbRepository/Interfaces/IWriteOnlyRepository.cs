using System;
namespace DbRepository.Interfaces
{
	public interface IWriteOnlyRepository<T> where T: class
	{
		public Task CreateAsync(T entity);
		public void Update(T entity);
		public Task DeleteAsync(long Id);
	}
}

