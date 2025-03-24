using AutoMapper;
using Data.Service;
using Data.UnitOfWork;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.Services.Interfaces;
using FluentValidation;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class CostDataService : BaseCrudDataService<Cost, Guid, CostRequestDto, CostResponseDto>, ICostDataService
    {
        public CostDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CostRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
