using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdineController : Controller
    {
        private readonly IOrdineRepository _ordineRepository;
        public OrdineController(IOrdineRepository ordineRepository)
        {
            _ordineRepository = ordineRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdineDTO>>> GetAll()
        {
            try
            {
                var ordini = await _ordineRepository.GetAllAsync();
                return Ok(ordini);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        // GET: api/ordine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdineDTO>> GetById(int id)
        {
            try
            {
                var ordine = await _ordineRepository.GetByIdAsync(id);

                if (ordine == null)
                {
                    return NotFound($"Ordine con ID {id} non trovato");
                }

                return Ok(ordine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        // POST: api/ordine
        [HttpPost]
        public async Task<ActionResult<OrdineDTO>> Create([FromBody] OrdineDTO ordineDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Set default values if needed
                ordineDTO.DataCreazione = DateTime.Now;
                ordineDTO.DataAggiornamento = DateTime.Now;

                var createdOrdine = await _ordineRepository.AddAsync(ordineDTO);

                return CreatedAtAction(nameof(GetById),
                    new { id = createdOrdine.OrdineId },
                    createdOrdine);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        // PUT: api/ordine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrdineDTO ordineDTO)
        {
            try
            {
                if (id != ordineDTO.OrdineId)
                {
                    return BadRequest("ID nell'URL non corrisponde all'ID nell'oggetto");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingOrdine = await _ordineRepository.GetByIdAsync(id);
                if (existingOrdine == null)
                {
                    return NotFound($"Ordine con ID {id} non trovato");
                }

                // Update data aggiornamento
                ordineDTO.DataAggiornamento = DateTime.Now;

                await _ordineRepository.UpdateAsync(ordineDTO);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        // DELETE: api/ordine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ordine = await _ordineRepository.GetByIdAsync(id);
                if (ordine == null)
                {
                    return NotFound($"Ordine con ID {id} non trovato");
                }

                await _ordineRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }
}
