using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Service;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DolceController : ControllerBase
    {
        private readonly IDolceRepository _repo;

        public DolceController(DolceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dolci = await _repo.GetAllAsync();
            return Ok(dolci);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dolce = await _repo.GetByIdAsync(id);
            if (dolce == null)
                return NotFound();
            return Ok(dolce);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DolceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repo.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ArticoloId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DolceDTO dto)
        {
            if (id != dto.ArticoloId)
                return BadRequest("L'ID non corrisponde.");

            try
            {
                await _repo.UpdateAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("priorita/{priorita}")]
        public async Task<IActionResult> GetByPriorita(int priorita)
        {
            var dolci = await _repo.GetByPrioritaAsync(priorita);
            return Ok(dolci);
        }
    }
}
