using AutoMapper;
using DbRepository;
using DTOs;
using FineBudget.Services.Interfaces;
using FineBudget.Services.Specifications;
using FineBudget.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class AccountDataService: IAccountDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Func<Account, bool> True;

        public AccountDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> CreateAsync(AccountRequestDto dto)
        {
            Account account = _mapper.Map<Account>(dto);

            var result = await _unitOfWork.AccountRepository.CreateAsync(account);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<AccountResponseDto>(result);

            return responseDto;
        }

        public async  Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.AccountRepository.DeleteAsync(id);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<AccountResponseDto> GetByIdAsync(Guid id)
        {
            var result = await _unitOfWork.AccountRepository.GetAsync(id);

            if (result == null) return null;

            AccountResponseDto response = _mapper.Map<AccountResponseDto>(result);

            return response;
        }

        public async  Task<List<AccountResponseDto>> GetAllAsync()
        {
            AccountSpecification specification = new AccountSpecification();

            var result = await _unitOfWork.AccountRepository.GetBySpecificationAsync(specification);

            var responseDto = new List<AccountResponseDto>();

            foreach (var item in result)
            {
                var responseItem = _mapper.Map<AccountResponseDto>(item);

                responseDto.Add(responseItem);
            }

            return responseDto;
        }

        public async Task<AccountResponseDto> UpdateAsync(Guid id, AccountRequestDto dto)
        {
            Account account = await _unitOfWork.AccountRepository.GetAsync(id);

            _mapper.Map(dto, account);

            var result = await _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AccountResponseDto>(result);
        }
    }
}
