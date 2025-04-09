using AutoMapper;
using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;
using FluentValidation;

namespace FineBudget.Data
{
    public class IncomeDataService : BaseCrudDataService<Income, Guid, IncomeRequestDto, IncomeResponseDto>, IIncomeDataService
    {
        public IncomeDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<IncomeRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
