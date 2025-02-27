using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs.Requests;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DbModels.Enums;
using Models.DbModels.MainModels;
using AutoMapper;
using FineBudget.Services.Interfaces;
using DTOs.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IncomesController : Controller
    {
        private readonly IIncomeDataService _incomeDataService;
        private readonly ILogger<IncomesController> _logger;

        public IncomesController(ILogger<IncomesController> logger, IIncomeDataService incomeDataService)
        {
            _incomeDataService = incomeDataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<IncomeResponseDto>> response = new ApiResponse<List<IncomeResponseDto>>();

            try
            {
                //var result = await _unitOfWork.IncomeRepository.GetAllAsync();

                return Ok();
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
            ApiResponse<IncomeResponseDto> response = new ApiResponse<IncomeResponseDto>();

            try
            {
                var result = await _incomeDataService.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] IncomeRequestDto dto)
        {
            ApiResponse<IncomeResponseDto> response = new ApiResponse<IncomeResponseDto>();

            try
            {
                var result = await _incomeDataService.CreateAsync(dto);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] IncomeRequestDto dto)
        {
            ApiResponse<IncomeResponseDto> response = new ApiResponse<IncomeResponseDto>();

            try
            {
                var result = await _incomeDataService.UpdateAsync(id, dto);

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
                var result = await _incomeDataService.DeleteAsync(id);

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
    }
}

