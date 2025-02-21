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
                var result = await _unitOfWork.CostRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                CostResponseDto response = _mapper.Map<CostResponseDto>(result);

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
                Cost cost = _mapper.Map<Cost>(dto);

                await _unitOfWork.CostRepository.CreateAsync(cost);
                await _unitOfWork.SaveAsync();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CostRequestDto dto)
        {
            ApiResponse<CostResponseDto> response = new ApiResponse<CostResponseDto>();

            try
            {
                Cost cost = await _unitOfWork.CostRepository.GetAsync(id);

                if (cost == null)
                    return NotFound();

                _mapper.Map(dto, cost);

                _unitOfWork.CostRepository.Update(cost);
                await _unitOfWork.SaveAsync();

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                await _unitOfWork.CostRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();

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
    }
}

