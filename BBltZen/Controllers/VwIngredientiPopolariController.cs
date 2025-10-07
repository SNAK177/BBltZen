using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{

    public class VwIngredientiPopolariController : Controller
    {
        private IVwIngredientiPopolariRepository _repository;
        public VwIngredientiPopolariController(IVwIngredientiPopolariRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwIngredientiPopolariDTO>>> GetAll()
        {
           var result = await _repository.GetAllAsync();
           return Ok(result);
        
        }

        [HttpGet("top/{n:int}")]
        public async Task<ActionResult<IEnumerable<VwIngredientiPopolariDTO>>> GetTopN(int n)
        {
            var result = await _repository.GetTopNAsync(n);
            return Ok(result);
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<VwIngredientiPopolariDTO>>> GetByCategoria(string categoria)
        {
            var result = await _repository.GetByCategoriaAsync(categoria);
            return Ok(result);
        }

        [HttpGet("{ingredienteId:int}")]
        public async Task<ActionResult<VwIngredientiPopolariDTO>> GetById(int ingredienteId)
        {
            var result = await _repository.GetByIngredienteIdAsync(ingredienteId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}