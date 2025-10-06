using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatoOrdineController : ControllerBase
    {
        private readonly IStatoOrdineRepository _repository;

        public StatoOrdineController(IStatoOrdineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatoOrdineDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StatoOrdineDTO>> GetById(int id)
        {
            var stato = await _repository.GetByIdAsync(id);
            if (stato == null)
                return NotFound();
            return Ok(stato);
        }

        [HttpGet("by-nome/{nome}")]
        public async Task<ActionResult<StatoOrdineDTO>> GetByNome(string nome)
        {
            var stato = await _repository.GetByNomeAsync(nome);
            if (stato == null)
                return NotFound();
            return Ok(stato);
        }

        [HttpGet("terminali")]
        public async Task<ActionResult<IEnumerable<StatoOrdineDTO>>> GetTerminali()
        {
            var result = await _repository.GetStatiTerminaliAsync();
            return Ok(result);
        }

        [HttpGet("non-terminali")]
        public async Task<ActionResult<IEnumerable<StatoOrdineDTO>>> GetNonTerminali()
        {
            var result = await _repository.GetStatiNonTerminaliAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StatoOrdineDTO>> Create(StatoOrdineDTO dto)
        {
            await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.StatoOrdineId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, StatoOrdineDTO dto)
        {
            if (id != dto.StatoOrdineId)
                return BadRequest("ID mismatch");

            if (!await _repository.ExistsAsync(id))
                return NotFound();

            await _repository.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
