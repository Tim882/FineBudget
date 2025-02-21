using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs.Requests;
using FineBudget.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
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
    public class LiabilitiesController : Controller
    {
        private readonly ILiabilityDataService _liabilityDataService;
        private readonly ILogger<LiabilitiesController> _logger;

        public LiabilitiesController(ILogger<LiabilitiesController> logger, ILiabilityDataService liabilityDataService)
        {
            _liabilityDataService = liabilityDataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<LiabilityResponseDto>> response = new ApiResponse<List<LiabilityResponseDto>>();

            try
            {
                //var result = await _unitOfWork.LiabilityRepository.GetAllAsync();

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
            ApiResponse<LiabilityResponseDto> response = new ApiResponse<LiabilityResponseDto>();

            try
            {
                var result = await _unitOfWork.LiabilityRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                LiabilityResponseDto response = _mapper.Map<LiabilityResponseDto>(result);

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
        public async Task<IActionResult> Create([FromBody] LiabilityRequestDto dto)
        {
            ApiResponse<LiabilityResponseDto> response = new ApiResponse<LiabilityResponseDto>();

            try
            {
                Liability liability = _mapper.Map<Liability>(dto);

                await _unitOfWork.LiabilityRepository.CreateAsync(liability);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] LiabilityRequestDto dto)
        {
            ApiResponse<LiabilityResponseDto> response = new ApiResponse<LiabilityResponseDto>();

            try
            {
                Liability liability = await _unitOfWork.LiabilityRepository.GetAsync(id);

                _mapper.Map(dto, liability);

                _unitOfWork.LiabilityRepository.Update(liability);
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
                await _unitOfWork.LiabilityRepository.DeleteAsync(id);
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

