using AutoMapper;
using Data.Models;
using Data.UnitOfWork;
using DbRepository.Specifications;
using FineBudget.Models;
using FluentValidation;

namespace Data.Service
{
    public class BaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> : IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto>
    where TEntity : class
    where TRequestDto : class
    where TResponseDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IValidator<TRequestDto> _validator;

        public BaseCrudDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TRequestDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PaginatedResponse<TResponseDto>> GetAsync(QueryParameters parameters)
        {
            var spec = new BaseSpecification<TEntity>(parameters);

            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entities = await repository.GetAsync(spec);
            var totalCount = await repository.CountAsync(spec);

            var dtos = _mapper.Map<IReadOnlyList<TResponseDto>>(entities);
            return new PaginatedResponse<TResponseDto>(dtos, totalCount, spec.Skip, spec.Take);
        }

        public async Task<TResponseDto> GetByIdAsync(TKey id)
        {
            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            return _mapper.Map<TResponseDto>(entity);
        }

        public async Task<TResponseDto> CreateAsync(TRequestDto dto)
        {
            // Валидация DTO
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entity = _mapper.Map<TEntity>(dto);
            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            await repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TResponseDto>(entity);
        }

        public async Task UpdateAsync(TKey id, TRequestDto dto)
        {
            // Валидация DTO
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            _mapper.Map(dto, entity);
            repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            await repository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
