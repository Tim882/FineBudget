using DTOs.Requests;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CostsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CostsController> _logger;

        public CostsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CostsController> logger)
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
                //var result = await _unitOfWork.CostRepository.GetAllAsync();

                return Ok();
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
                var result = await _unitOfWork.CostRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                CostResponseDto response = _mapper.Map<CostResponseDto>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CostRequestDto dto)
        {
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CostRequestDto dto)
        {
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _unitOfWork.CostRepository.DeleteAsync(id);
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

