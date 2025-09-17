using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DolceController : Controller
    {
        private readonly IDolceRepository _dolceRepository;

        public DolceController(IDolceRepository dolceRepository)
        {
            _dolceRepository = dolceRepository;
        }

        // GET: api/Dolce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DolceDTO>>> GetAll()
        {
            var dolci = await _dolceRepository.GetAllAsync();
            return Ok(dolci);
        }

        // GET: api/Dolce/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DolceDTO>> GetById(int id)
        {
            var dolce = await _dolceRepository.GetByIdAsync(id);
            if (dolce == null) return NotFound();
            return Ok(dolce);
        }

        // POST: api/Dolce
        [HttpPost]
        public async Task<ActionResult<DolceDTO>> Create([FromBody] DolceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _dolceRepository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ArticoloId }, created);
        }

        // PUT: api/Dolce/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DolceDTO dto)
        {
            if (id != dto.ArticoloId)
                return BadRequest("ID mismatch");

            try
            {
                await _dolceRepository.UpdateAsync(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Dolce/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _dolceRepository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // PATCH: api/Dolce/5/toggle-disponibilita
        [HttpPatch("{id:int}/toggle-disponibilita")]
        public async Task<IActionResult> ToggleDisponibilita(int id, [FromQuery] bool disponibile)
        {
            var success = await _dolceRepository.ToggleDisponibilitaAsync(id, disponibile);
            if (!success) return NotFound();
            return NoContent();
        }

        // GET: api/Dolce/disponibili
        [HttpGet("disponibili")]
        public async Task<ActionResult<IEnumerable<DolceDTO>>> GetDisponibili()
        {
            var dolci = await _dolceRepository.GetDisponibiliAsync();
            return Ok(dolci);
        }

        // GET: api/Dolce/priorita/3
        [HttpGet("priorita/{priorita:int}")]
        public async Task<ActionResult<IEnumerable<DolceDTO>>> GetByPriorita(int priorita)
        {
            var dolci = await _dolceRepository.GetByPrioritaAsync(priorita);
            return Ok(dolci);
        }
    }
}
