using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRatesController : Controller
    {
        private readonly ITaxRatesRepository _repository;
        public TaxRatesController(ITaxRatesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRatesDTO>>> GetAll()
        {
            var taxRates = await _repository.GetAllAsync();
            return Ok(taxRates);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaxRatesDTO>> GetById(int id)
        {
            var taxRate = await _repository.GetByIdAsync(id);
            if (taxRate == null) return NotFound();
            return Ok(taxRate);
        }

        [HttpGet("aliquota/{aliquota:decimal}")]
        public async Task<ActionResult<TaxRatesDTO>> GetByAliquota(decimal aliquota)
        {
            var taxRate = await _repository.GetByAliquotaAsync(aliquota);
            if (taxRate == null) return NotFound();
            return Ok(taxRate);
        }

        [HttpPost]
        public async Task<ActionResult<TaxRatesDTO>> Create(TaxRatesDTO dto)
        {
            await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.TaxRateId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, TaxRatesDTO dto)
        {
            if (id != dto.TaxRateId) return BadRequest();

            var exists = await _repository.ExistsAsync(id);
            if (!exists) return NotFound();

            await _repository.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}