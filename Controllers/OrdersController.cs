using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public OrdersController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.Orders
                .OrderBy(o => o.Order_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => OrderDto.ToDto(o))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(OrderDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var item = new Order
            {
                User_Id = dto.User_Id,
                OrderDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Order_Id }, OrderDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null) return NotFound();
            item.User_Id = dto.User_Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchOrderDto dto)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.User_Id.HasValue) item.User_Id = dto.User_Id.Value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Orders.FindAsync(id);
            if (item == null) return NotFound();
            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}