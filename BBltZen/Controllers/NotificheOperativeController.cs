using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Service;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificheOperativeController : Controller
    {
        private readonly INotificheOperativeRepository _repository;

        public NotificheOperativeController(INotificheOperativeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificheOperativeDTO>>> GetAll()
        {
            var notifiche = await _repository.GetAllAsync();
            return Ok(notifiche);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotificheOperativeDTO>> GetById(int id)
        {
            var notifica = await _repository.GetByIdAsync(id);
            if (notifica == null) return NotFound();
            return Ok(notifica);
        }

        [HttpGet("stato/{stato}")]
        public async Task<ActionResult<IEnumerable<NotificheOperativeDTO>>> GetByStato(string stato)
        {
            var notifiche = await _repository.GetByStatoAsync(stato);
            return Ok(notifiche);
        }

        [HttpGet("priorità/{priorita:int}")]
        public async Task<ActionResult<IEnumerable<NotificheOperativeDTO>>> GetByPriorita(int priotita)
        {
            var notifiche = await _repository.GetByPrioritaAsync(priotita);
            return Ok(notifiche);
        }

        [HttpGet("pendenti")]
        public async Task<ActionResult<IEnumerable<NotificheOperativeDTO>>> GetPendenti()
        {
            var notifiche = await _repository.GetPendentiAsync();
            return Ok(notifiche);
        }

        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<NotificheOperativeDTO>>> GetPeriode(DateTime dataInizio,
            DateTime dataFine)
        {
            if (dataInizio > dataFine)
                return BadRequest("La data di inizio non può essere successiva alla data di fine.");
            var notifiche = await _repository.GetByPeriodoAsync(dataInizio, dataFine);
            return Ok(notifiche);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NotificheOperativeDTO notifica)
        {
            await _repository.AddAsync(notifica);
            return CreatedAtAction(nameof(GetById), new { id = notifica.NotificaId }, notifica);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, NotificheOperativeDTO notifica)
        {
            if (id != notifica.NotificaId)
            {
                return BadRequest("ID mismatch.");
            }

            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(notifica);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notifica = await _repository.GetByIdAsync(id);
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}