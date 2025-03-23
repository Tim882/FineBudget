using AutoMapper;
using Data.Models;
using Data.UnitOfWork;
using DbRepository.Specifications;
using FineBudget.Models;
using FluentValidation;

namespace Data.Service
{
    public class BaseCrudDataService<TEntity, TKey, TDto> : IBaseCrudDataService<TEntity, TKey, TDto>
    where TEntity : class
    where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public BaseCrudDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PaginatedResponse<TDto>> GetAsync(QueryParameters parameters)
        {
            var spec = new BaseSpecification<TEntity>(parameters);

            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entities = await repository.GetAsync(spec);
            var totalCount = await repository.CountAsync(spec);

            var dtos = _mapper.Map<IReadOnlyList<TDto>>(entities);
            return new PaginatedResponse<TDto>(dtos, totalCount, spec.Skip, spec.Take);
        }

        public async Task<TDto> GetByIdAsync(TKey id)
        {
            var repository = _unitOfWork.GetRepository<TEntity, TKey>();
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
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

            return _mapper.Map<TDto>(entity);
        }

        public async Task UpdateAsync(TKey id, TDto dto)
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
