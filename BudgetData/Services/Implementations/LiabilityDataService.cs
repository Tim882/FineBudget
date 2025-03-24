using AutoMapper;
using Data.Service;
using Data.UnitOfWork;
using DbRepository;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FluentValidation;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class LiabilityDataService : BaseCrudDataService<Liability, Guid, LiabilityRequestDto, LiabilityResponseDto>, ILiabilityDataService
    {
        public LiabilityDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LiabilityRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
