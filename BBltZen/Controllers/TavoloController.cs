using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TavoloController : ControllerBase
    {
        private readonly ITavoloRepository _repository;

        public TavoloController(ITavoloRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TavoloDTO>>> GetAll()
        {
            var tavoli = await _repository.GetAllAsync();
            return Ok(tavoli);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TavoloDTO>> GetById(int id)
        {
            var tavolo = await _repository.GetByIdAsync(id);
            if (tavolo == null)
                return NotFound();
            return Ok(tavolo);
        }

        [HttpGet("by-numero/{numero:int}")]
        public async Task<ActionResult<TavoloDTO>> GetByNumero(int numero)
        {
            var tavolo = await _repository.GetByNumeroAsync(numero);
            if (tavolo == null)
                return NotFound();
            return Ok(tavolo);
        }

        [HttpGet("by-qrcode/{qrCode}")]
        public async Task<ActionResult<TavoloDTO>> GetByQrCode(string qrCode)
        {
            var tavolo = await _repository.GetByQrCodeAsync(qrCode);
            if (tavolo == null)
                return NotFound();
            return Ok(tavolo);
        }

        [HttpGet("zona/{zona}")]
        public async Task<ActionResult<IEnumerable<TavoloDTO>>> GetByZona(string zona)
        {
            var tavoli = await _repository.GetByZonaAsync(zona);
            return Ok(tavoli);
        }

        [HttpGet("disponibili")]
        public async Task<ActionResult<IEnumerable<TavoloDTO>>> GetDisponibili()
        {
            var tavoli = await _repository.GetDisponibiliAsync();
            return Ok(tavoli);
        }

        [HttpPost]
        public async Task<ActionResult<TavoloDTO>> Create(TavoloDTO dto)
        {
            await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.TavoloId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, TavoloDTO dto)
        {
            if (id != dto.TavoloId)
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
