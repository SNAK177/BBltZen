using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StatoPagamentoController : Controller
    {
        private readonly IStatoPagamentoRepository _repository;
        public StatoPagamentoController(IStatoPagamentoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatoPagamentoDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StatoPagamentoDTO>> GetById(int id)
        {
            var stato = await _repository.GetByIdAsync(id);
            if (stato == null)
                return NotFound();

            return Ok(stato);
        }

        // GET: api/StatoPagamento/by-nome/{nome}
        [HttpGet("by-nome/{nome}")]
        public async Task<ActionResult<StatoPagamentoDTO>> GetByNome(string nome)
        {
            var stato = await _repository.GetByNomeAsync(nome);
            if (stato == null)
                return NotFound();

            return Ok(stato);
        }

        // POST: api/StatoPagamento
        [HttpPost]
        public async Task<ActionResult<StatoPagamentoDTO>> Create(StatoPagamentoDTO dto)
        {
            await _repository.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = dto.StatoPagamentoId }, dto);
        }

        // PUT: api/StatoPagamento/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, StatoPagamentoDTO dto)
        {
            if (id != dto.StatoPagamentoId)
                return BadRequest("ID mismatch");

            try
            {
                await _repository.UpdateAsync(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/StatoPagamento/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}