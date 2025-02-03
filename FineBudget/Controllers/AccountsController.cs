using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using DTOs.Responses;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //var result = await _unitOfWork.AccountRepository.GetAllAsync();

                ApiResponse<List<Account>> response = new ApiResponse<List<Account>>();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await _unitOfWork.AccountRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                AccountResponseDto response = _mapper.Map<AccountResponseDto>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequestDto dto)
        {
            try
            {
                Account account = _mapper.Map<Account>(dto);

                await _unitOfWork.AccountRepository.CreateAsync(account);
                await _unitOfWork.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] AccountRequestDto dto)
        {
            try
            {
                Account account = await _unitOfWork.AccountRepository.GetAsync(id);

                _mapper.Map(dto, account);

                _unitOfWork.AccountRepository.Update(account);
                await _unitOfWork.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                await _unitOfWork.AccountRepository.DeleteAsync(Id);
                await _unitOfWork.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

