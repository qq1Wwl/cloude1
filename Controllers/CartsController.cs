using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CartsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.Carts
                .OrderBy(c => c.Cart_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => CartDto.ToDto(c))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDto>> GetById(int id)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(CartDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> Create([FromBody] CreateCartDto dto)
        {
            var item = new Cart
            {
                User_Id = dto.User_Id,
                CreatedAt = DateTime.UtcNow
            };
            _context.Carts.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Cart_Id }, CartDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartDto dto)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item == null) return NotFound();
            item.User_Id = dto.User_Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchCartDto dto)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.User_Id.HasValue) item.User_Id = dto.User_Id.Value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item == null) return NotFound();
            _context.Carts.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}