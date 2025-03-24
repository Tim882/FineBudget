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
    public interface IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> where TEntity : class where TRequestDto : class where TResponseDto : class
    {
        Task<PaginatedResponse<TResponseDto>> GetAsync(QueryParameters parameters);
        Task<TResponseDto> GetByIdAsync(TKey id);
        Task<TResponseDto> CreateAsync(TRequestDto dto);
        Task UpdateAsync(TKey id, TRequestDto dto);
        Task DeleteAsync(TKey id);
    }
}
