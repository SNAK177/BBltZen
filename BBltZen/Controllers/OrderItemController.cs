using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _repository;
        public OrderItemController(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemDTO>> GetById(int id)
        {
            var orderItem = await _repository.GetByIdAsync(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDTO>> Create(OrderItemDTO orderItemDto)
        {
            await _repository.AddAsync(orderItemDto);
            return CreatedAtAction(nameof(GetById), new { id = orderItemDto.OrderItemId }, orderItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItemDTO orderItem)
        {
            if(id != orderItem.OrderItemId)
                return BadRequest("ID mismatch");
            if(!await _repository.ExistsAsync(id))
                return NotFound();
            await _repository.UpdateAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!await _repository.ExistsAsync(id))
                return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("by-ordine/{ordineId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetByOrderId(int ordineId)
        {
            var orderItem = await _repository.GetByOrderIdAsync(ordineId);
            return Ok(orderItem);
        }
        [HttpGet("ByArticolo/{articoloId:int}")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetByArticoloId(int articoloId)
        {
            var orderItem = await _repository.GetByArticoloIdAsync(articoloId);
            return Ok(orderItem);
        }
    }
}