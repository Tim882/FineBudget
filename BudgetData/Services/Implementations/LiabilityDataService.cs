using AutoMapper;
using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;
using FluentValidation;

namespace FineBudget.Data
{
    public class LiabilityDataService : BaseCrudDataService<Liability, Guid, LiabilityRequestDto, LiabilityResponseDto>, ILiabilityDataService
    {
        public LiabilityDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LiabilityRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
