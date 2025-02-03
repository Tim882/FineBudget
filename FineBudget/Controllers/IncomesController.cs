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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IncomesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<IncomesController> _logger;

        public IncomesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<IncomesController> logger)
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
                //var result = await _unitOfWork.IncomeRepository.GetAllAsync();

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
                var result = await _unitOfWork.IncomeRepository.GetAsync(id);

                if (result == null)
                    return NotFound();

                IncomeResponseDto response = _mapper.Map<IncomeResponseDto>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncomeRequestDto dto)
        {
            try
            {
                Income income = _mapper.Map<Income>(dto);

                await _unitOfWork.IncomeRepository.CreateAsync(income);
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
        public async Task<IActionResult> Update(long id, [FromBody] IncomeRequestDto dto)
        {
            try
            {
                Income income = await _unitOfWork.IncomeRepository.GetAsync(id);

                if (income == null)
                    return NotFound();

                _mapper.Map(dto, income);

                _unitOfWork.IncomeRepository.Update(income);
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
                await _unitOfWork.IncomeRepository.DeleteAsync(id);
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

