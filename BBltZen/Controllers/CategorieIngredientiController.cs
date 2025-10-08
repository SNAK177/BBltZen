using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repository.Service;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategorieIngredientiController : Controller
    {
        private readonly CategorieIngredientiRepository _repository;

        public CategorieIngredientiController(CategorieIngredientiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound($"Categoria con ID {id} non trovata.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategorieIngredientiDTO dto)
        {
            if (dto == null)
                return BadRequest("Categoria non valida.");

            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoriaId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategorieIngredientiDTO dto)
        {
            if (id != dto.CategoriaId)
                return BadRequest("ID mismatch.");

            await _repository.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
