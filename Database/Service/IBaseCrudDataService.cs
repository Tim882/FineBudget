using Base.Models;

namespace Base.Database
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
