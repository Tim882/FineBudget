using AutoMapper;
using DTOs;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class AccountDataService: IAccountDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
            //var result = await _unitOfWork.AccountRepository.GetAllAsync();

            var responseDto = new List<AccountResponseDto>();

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
