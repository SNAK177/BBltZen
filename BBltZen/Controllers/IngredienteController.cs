using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : Controller
    {
        private readonly IIngredienteRepository _repository;
        public IngredienteController(IIngredienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngredienteDTO>> GetById(int id)
        {
            var ingrediente = await _repository.GetByIdAsync(id);
            if (ingrediente == null) return NotFound();
            return Ok(ingrediente);
        }

        [HttpGet("by-disponibilità/{dispibile:bool}")]
        public async Task<ActionResult<IngredienteDTO>> GetByDisponibilita(bool disponibile)
        {
            var stato = await _repository.GetDisponibiliAsync(disponibile);
            if(stato == null) return NotFound();
            return Ok(stato);
        }
    }
}