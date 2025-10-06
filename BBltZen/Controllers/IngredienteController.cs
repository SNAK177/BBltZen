using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : Controller
    {
        private readonly IIngredienteRepository _repository;
        public IngredienteController(IIngredienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngredienteDTO>> GetById(int id)
        {
            var ingrediente = await _repository.GetByIdAsync(id);
            if (ingrediente == null) return NotFound();
            return Ok(ingrediente);
        }

        [HttpGet("by-disponibilita/{disponibile:bool}")]
        public async Task<ActionResult<IngredienteDTO>> GetByDisponibilita(bool disponibile)
        {
            var stato = await _repository.GetDisponibiliAsync(disponibile);
            if(stato == null || !stato.Any()) 
                return NotFound("nessun ingrediente trovato");
            return Ok(stato) ;
        }
        [HttpPost]
        public async Task<ActionResult> Create(IngredienteDTO ingrediente)
        {
            await _repository.AddAsync(ingrediente);
            return CreatedAtAction(nameof(GetById), new { id = ingrediente.IngredienteId }, ingrediente);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] IngredienteDTO ingrediente)
        {
            if (id != ingrediente.IngredienteId)
            {
                return BadRequest("ID mismatch");
            }
            await _repository.UpdateAsync(ingrediente);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingIngrediente = await _repository.GetByIdAsync(id);
            if (existingIngrediente == null)
            {
                return NotFound($"Ingrediente con ID {id} non trovato");
            }
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Errore durante la cancellazione: {e.Message}");
            }
        }
    }
}