using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/cartitems")]
    public class CartItemsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CartItemsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.CartItems
                .OrderBy(ci => ci.CartItem_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(ci => CartItemDto.ToDto(ci))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetById(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(CartItemDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> Create([FromBody] CreateCartItemDto dto)
        {
            var item = new CartItem
            {
                Cart_Id = dto.Cart_Id,
                Product_Id = dto.Product_Id,
                Quantity = dto.Quantity,
                CreatedAt = DateTime.UtcNow
            };
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.CartItem_Id }, CartItemDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartItemDto dto)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();
            item.Cart_Id = dto.Cart_Id;
            item.Product_Id = dto.Product_Id;
            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchCartItemDto dto)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.Cart_Id.HasValue) item.Cart_Id = dto.Cart_Id.Value;
            if (dto.Product_Id.HasValue) item.Product_Id = dto.Product_Id.Value;
            if (dto.Quantity.HasValue) item.Quantity = dto.Quantity.Value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}