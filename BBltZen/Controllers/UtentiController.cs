using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : Controller
    {
        private readonly IUtentiRepository _repository;
        public UtentiController(IUtentiRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtentiDTO>>> GetAll()
        {
            var utenti = await _repository.GetAllAsync();
            return Ok(utenti);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UtentiDTO>> GetById(int id)
        {
            var utente = await _repository.GetByIdAsync(id);
            if (utente == null) return NotFound();
            return Ok(utente);
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UtentiDTO>> GetByEmail(string email)
        {
            var utente = await _repository.GetByEmailAsync(email);
            if (utente == null) return NotFound();
            return Ok(utente);
        }
        [HttpGet("tipo/{tipoUtente}")]
        public async Task<ActionResult<IEnumerable<UtentiDTO>>> GetByTipo(string tipoUtente)
        {
            var utenti = await _repository.GetByTipoUtenteAsync(tipoUtente);
            return Ok(utenti);
        }
        [HttpGet("attivi")]
        public async Task<ActionResult<IEnumerable<UtentiDTO>>> GetAttivi()
        {
            var utenti = await _repository.GetAttiviAsync();
            return Ok(utenti);
        }
        [HttpPost]
        public async Task<ActionResult> Create(UtentiDTO utente)
        {
            if (await _repository.EmailExistsAsync(utente.Email))
            {
                return Conflict("Email already exists.");
            }
            await _repository.AddAsync(utente);
            return CreatedAtAction(nameof(GetById), new { id = utente.UtenteId }, utente);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UtentiDTO utente)
        {
            if (id != utente.UtenteId)
            {
                return BadRequest("ID mismatch.");
            }
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }
            await _repository.UpdateAsync(utente);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var utente= await _repository.GetByIdAsync(id);
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }
            utente.Attivo = false;
            utente.DataAggiornamento = DateTime.UtcNow;
            await _repository.UpdateAsync(utente);
            return NoContent();
        }
    }
}
