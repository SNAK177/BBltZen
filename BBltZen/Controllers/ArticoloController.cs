using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Service;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticoloController : Controller
    {
        private readonly IArticoloRepository _repository;

        public ArticoloController(IArticoloRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ArticoloDTO>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ArticoloDTO>> GetById(int id)
        {
            var articolo = await _repository.GetByIdAsync(id);
            if (articolo == null)
                return NotFound();
            return Ok(articolo);
        }

        [HttpPost]
        public async Task<ActionResult<ArticoloDTO>> Create([FromBody] ArticoloDTO articoloDto)
        {
            if (articoloDto == null)
                return BadRequest();
            var created = await _repository.AddAsync(articoloDto);
            return CreatedAtAction(nameof(GetById), new { id = created.ArticoloId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticoloDTO articolo)
        {
            if (id != articolo.ArticoloId)
                return BadRequest();
            var updated = await _repository.UpdateAsync(articolo);
            if(!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if(!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
