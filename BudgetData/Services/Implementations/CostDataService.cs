using AutoMapper;
using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;
using FluentValidation;

namespace FineBudget.Data
{
    public class CostDataService : BaseCrudDataService<Cost, Guid, CostRequestDto, CostResponseDto>, ICostDataService
    {
        public CostDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CostRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
