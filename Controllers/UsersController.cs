using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public UsersController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.Users
                .OrderBy(u => u.User_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => UserDto.ToDto(u))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(UserDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var item = new User
            {
                Login = dto.Login,
                PasswordHash = dto.PasswordHash,
                Role_Id = dto.Role_Id,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.User_Id }, UserDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            item.Login = dto.Login;
            item.PasswordHash = dto.PasswordHash;
            item.Role_Id = dto.Role_Id;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchUserDto dto)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.Login != null) item.Login = dto.Login;
            if (dto.PasswordHash != null) item.PasswordHash = dto.PasswordHash;
            if (dto.Role_Id.HasValue) item.Role_Id = dto.Role_Id.Value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}