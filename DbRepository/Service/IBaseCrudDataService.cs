using Data.Models;
using DbRepository.Specifications;
using FineBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IBaseCrudDataService<TEntity, TKey, TDto> where TEntity : class where TDto : class
    {
        Task<PaginatedResponse<TDto>> GetAsync(QueryParameters parameters);
        Task<TDto> GetByIdAsync(TKey id);
        Task<TDto> CreateAsync(TDto dto);
        Task UpdateAsync(TKey id, TDto dto);
        Task DeleteAsync(TKey id);
    }
}
