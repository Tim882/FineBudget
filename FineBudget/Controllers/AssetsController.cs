using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.Models;
using FineBudget.Services.Interfaces;
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
        private readonly IAssetDataService _assetDataService;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(ILogger<AssetsController> logger, IAssetDataService assetDataService)
        {
            _assetDataService = assetDataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        {
            ApiResponse<List<AssetResponseDto>> response = new ApiResponse<List<AssetResponseDto>>();

            try
            {
                var result = await _assetDataService.GetAllAsync();

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResponse<AssetResponseDto> response = new ApiResponse<AssetResponseDto>();

            try
            {
                var result = await _assetDataService.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] AssetRequestDto dto)
        {
            ApiResponse<AssetResponseDto> response = new ApiResponse<AssetResponseDto>();

            try
            {
                var result = await _assetDataService.CreateAsync(dto);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] AssetRequestDto dto)
        {
            ApiResponse<AssetResponseDto> response = new ApiResponse<AssetResponseDto>();

            try
            {
                AssetResponseDto asset = await _assetDataService.UpdateAsync(id, dto);

                if (asset == null)
                    return NotFound();

                response.Data = asset;

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
                var result = await _assetDataService.DeleteAsync(id);

                if (result) return Ok(response);

                return StatusCode(500, response);
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

