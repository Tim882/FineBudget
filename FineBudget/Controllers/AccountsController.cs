using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using DTOs.Responses;
using FineBudget.Services.Interfaces;
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
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountDataService _accountDataService;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountDataService accountDataService)
        {
            _logger = logger;
            _accountDataService = accountDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<AccountResponseDto>> response = new ApiResponse<List<AccountResponseDto>>();

            try
            {
                //var result = await _unitOfWork.AccountRepository.GetAllAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Message = ex.Message;

                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResponse<AccountResponseDto> response = new ApiResponse<AccountResponseDto>();

            try
            {
                var result = await _accountDataService.GetByIdAsync(id);

                if (result == null)
                    return NotFound();

                response.Data = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Message = ex.Message;

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequestDto dto)
        {
            ApiResponse<AccountResponseDto> response = new ApiResponse<AccountResponseDto>();

            try
            {
                var result = await _accountDataService.CreateAsync(dto);

                response.Data = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Message = ex.Message;

                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AccountRequestDto dto)
        {
            ApiResponse<AccountResponseDto> response = new ApiResponse<AccountResponseDto>();

            try
            {
                var result = await _accountDataService.UpdateAsync(id, dto);
                
                response.Data = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Message = ex.Message;

                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                response.Data = await _accountDataService.DeleteAsync(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Message = ex.Message;

                return StatusCode(500, response);
            }
        }
    }
}

