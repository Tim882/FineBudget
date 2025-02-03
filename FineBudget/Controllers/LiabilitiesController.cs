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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LiabilitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LiabilitiesController> _logger;

        public LiabilitiesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LiabilitiesController> logger)
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
                //var result = await _unitOfWork.LiabilityRepository.GetAllAsync();

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
                var result = await _unitOfWork.LiabilityRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                LiabilityResponseDto response = _mapper.Map<LiabilityResponseDto>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LiabilityRequestDto dto)
        {
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] LiabilityRequestDto dto)
        {
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _unitOfWork.LiabilityRepository.DeleteAsync(id);
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

