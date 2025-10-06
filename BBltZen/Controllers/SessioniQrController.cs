using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessioniQrController : Controller
    {
        private readonly ISessioniQrRepository _repository;
        public SessioniQrController(ISessioniQrRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessioniQrDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid")]
        public async Task<ActionResult<SessioniQrDTO>> GetById(Guid id)
        {
            var sessione = await _repository.GetByIdAsync(id);
            if (sessione == null) return NotFound();
            return Ok(sessione);
        }

        [HttpGet("by-qrcode/{qrCode}")]
        public async Task<ActionResult<SessioniQrDTO>> GetByQrCode(string qrCode)
        {
            var sessione = await _repository.GetByQrCodeAsync(qrCode);
            if (sessione == null) return NotFound();
            return Ok(sessione);       
        }

        [HttpGet("non-utilizzate")]
        public async Task<ActionResult<IEnumerable<SessioniQrDTO>>> GetNonUtilizzate()
        {
            var sessioni = await _repository.GetNonutilizzateAsync();
            return Ok(sessioni);       
        }
        [HttpGet("scadute")]
        public async Task<ActionResult<IEnumerable<SessioniQrDTO>>> GetScadute()
        {
            var sessioni = await _repository.GetScaduteAsync();
            return Ok(sessioni);       
        }
        [HttpPost]
        public async Task<ActionResult> Create(SessioniQrDTO sessione)
        {
            await _repository.AddAsync(sessione);
            return CreatedAtAction(nameof(GetById), new { id = sessione.SessioneId }, sessione);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, SessioniQrDTO sessione)
        {
            if (id != sessione.SessioneId)
                return BadRequest("ID mismatch");
            
            if(!await _repository.ExistsAsync(id))
                return NotFound();
            
            await _repository.UpdateAsync(sessione);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(!await _repository.ExistsAsync(id))
                return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}