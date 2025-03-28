using FineBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Service;
using DTOs.Responses;
using ErrorsHandlers;
using Data.Models;

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseCrudController<TEntity, TKey, TRequestDto, TResponseDto> : ControllerBase
    where TEntity : class
    where TRequestDto : class
    where TResponseDto : class
    {
        protected readonly IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> _service;

        public BaseCrudController(IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryParameters parameters)
        {
            PaginatedResponse<TResponseDto> result = await _service.GetAsync(parameters);
            return Ok(ApiResponse<PaginatedResponse<TResponseDto>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(TKey id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TRequestDto dto)
        {
            var createdDto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = (createdDto as dynamic).Id }, ApiResponse<TResponseDto>.Ok(createdDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TKey id, TRequestDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TKey id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
