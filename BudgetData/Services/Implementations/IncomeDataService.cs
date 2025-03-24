using AutoMapper;
using Data.Service;
using Data.UnitOfWork;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FluentValidation;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class IncomeDataService : BaseCrudDataService<Income, Guid, IncomeRequestDto, IncomeResponseDto>, IIncomeDataService
    {
        public IncomeDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<IncomeRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
