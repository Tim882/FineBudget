using AutoMapper;
using Data.Service;
using Data.UnitOfWork;
using DbRepository;
using DTOs;
using FineBudget.Services.Interfaces;
using FineBudget.Services.Specifications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class AccountDataService: BaseCrudDataService<Account, Guid, AccountRequestDto, AccountResponseDto>, IAccountDataService
    {
        public AccountDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AccountRequestDto> validator) 
            : base(unitOfWork, mapper, validator) { }
    }
}
