using DTOs.Requests;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;
using AutoMapper;
using FineBudget.Services.Interfaces;
using DTOs.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CostsController : Controller
    {
        private readonly ICostDataService _costDataService;
        private readonly ILogger<CostsController> _logger;

        public CostsController(ILogger<CostsController> logger, ICostDataService costDataService)
        {
            _costDataService = costDataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<CostResponseDto>> response = new ApiResponse<List<CostResponseDto>>();

            try
            {
                //var result = await _unitOfWork.CostRepository.GetAllAsync();

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
            ApiResponse<CostResponseDto> response = new ApiResponse<CostResponseDto>();

            try
            {
                var result = await _costDataService.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] CostRequestDto dto)
        {
            ApiResponse<CostResponseDto> response = new ApiResponse<CostResponseDto>();

            try
            {
                var result = await _costDataService.CreateAsync(dto);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] CostRequestDto dto)
        {
            ApiResponse<CostResponseDto> response = new ApiResponse<CostResponseDto>();

            try
            {
                bool updated = await _costDataService.UpdateAsync(id, dto);

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
                bool result = await _costDataService.DeleteAsync(id);

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

