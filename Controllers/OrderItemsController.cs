using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/orderitems")]
    public class OrderItemsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public OrderItemsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.OrderItems
                .OrderBy(oi => oi.OrderItem_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(oi => OrderItemDto.ToDto(oi))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemDto>> GetById(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(OrderItemDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> Create([FromBody] CreateOrderItemDto dto)
        {
            var item = new OrderItem
            {
                Order_Id = dto.Order_Id,
                Product_Id = dto.Product_Id,
                Quantity = dto.Quantity,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.OrderItem_Id }, OrderItemDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemDto dto)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();
            item.Order_Id = dto.Order_Id;
            item.Product_Id = dto.Product_Id;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchOrderItemDto dto)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.Order_Id.HasValue) item.Order_Id = dto.Order_Id.Value;
            if (dto.Product_Id.HasValue) item.Product_Id = dto.Product_Id.Value;
            if (dto.Quantity.HasValue) item.Quantity = dto.Quantity.Value;
            if (dto.Price.HasValue) item.Price = dto.Price.Value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}