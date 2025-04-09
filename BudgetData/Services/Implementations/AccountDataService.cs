using AutoMapper;
using Base.Database;
using FineBudget.Data;
using FineBudget.DTO;
using FineBudget.Models;
using FluentValidation;

namespace FineBudget.Data
{
    public class AccountDataService: BaseCrudDataService<Account, Guid, AccountRequestDto, AccountResponseDto>, IAccountDataService
    {
        public AccountDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AccountRequestDto> validator) 
            : base(unitOfWork, mapper, validator) { }
    }
}
