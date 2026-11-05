using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public RolesController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.Roles
                .OrderBy(r => r.Role_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => RoleDto.ToDto(r))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var item = await _context.Roles.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(RoleDto.ToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
        {
            var item = new Role
            {
                RoleName = dto.RoleName,
                CreatedAt = DateTime.UtcNow
            };
            _context.Roles.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Role_Id }, RoleDto.ToDto(item));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto dto)
        {
            var item = await _context.Roles.FindAsync(id);
            if (item == null) return NotFound();
            item.RoleName = dto.RoleName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchRoleDto dto)
        {
            var item = await _context.Roles.FindAsync(id);
            if (item == null) return NotFound();
            if (dto.RoleName != null) item.RoleName = dto.RoleName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Roles.FindAsync(id);
            if (item == null) return NotFound();
            _context.Roles.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}