using FineBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Service;
using DTOs.Responses;
using ErrorsHandlers;
using Data.Models;
using DTOs.BaseDto;

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class BaseCrudController<TEntity, TKey, TRequestDto, TResponseDto> : ControllerBase
    where TEntity : class
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
    {
        protected readonly IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> _service;

        public BaseCrudController(IBaseCrudDataService<TEntity, TKey, TRequestDto, TResponseDto> service)
        {
            _service = service;
        }

        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<TResponseDto>>>> Get([FromQuery] QueryParameters _)
        {
            var parameters = new QueryParameters().ParseQueryParameters(Request);

            PaginatedResponse<TResponseDto> result = await _service.GetAsync(parameters);
            return Ok(ApiResponse<PaginatedResponse<TResponseDto>>.Ok(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TResponseDto>>> GetById(TKey id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(ApiResponse<TResponseDto>.Ok(dto));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse<TResponseDto>>> Create(TRequestDto dto)
        {
            var createdDto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = (createdDto as dynamic).Id }, ApiResponse<TResponseDto>.Ok(createdDto));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(TKey id, TRequestDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(TKey id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
