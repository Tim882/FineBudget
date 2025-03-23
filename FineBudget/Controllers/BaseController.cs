using FineBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Service;

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseCrudController<TEntity, TKey, TDto> : ControllerBase
    where TEntity : class
    where TDto : class
    {
        private readonly IBaseCrudDataService<TEntity, TKey, TDto> _service;

        public BaseCrudController(IBaseCrudDataService<TEntity, TKey, TDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryParameters parameters)
        {
            var result = await _service.GetAsync(parameters);
            return Ok(result);
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
        public async Task<IActionResult> Create(TDto dto)
        {
            var createdDto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = (createdDto as dynamic).Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TKey id, TDto dto)
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
