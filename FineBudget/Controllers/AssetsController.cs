using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AssetsController> logger)
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
                //var result = await _unitOfWork.AssetRepository.GetAllAsync();

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
                var result = await _unitOfWork.AssetRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                AssetResponseDto response = _mapper.Map<AssetResponseDto>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssetRequestDto dto)
        {
            try
            {
                Asset asset = _mapper.Map<Asset>(dto);

                await _unitOfWork.AssetRepository.CreateAsync(asset);
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
        public async Task<IActionResult> Update(long id, [FromBody] AssetRequestDto dto)
        {
            try
            {
                Asset asset = await _unitOfWork.AssetRepository.GetAsync(id);

                if (asset == null)
                    return NotFound();

                _mapper.Map(dto, asset);

                _unitOfWork.AssetRepository.Update(asset);
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
                await _unitOfWork.AssetRepository.DeleteAsync(id);
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

