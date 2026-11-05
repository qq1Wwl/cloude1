using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr9ASP.Data;
using pr9ASP.DTOs;
using pr9ASP.Models;

namespace pr9ASP.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var products = await _context.Products
                .OrderBy(p => p.Product_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => ProductDto.ToDto(p))
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Product_Id == id);

            if (product == null)
            {
                return NotFound(new { message = $"Товар с Id={id} не найден" });
            }

            return Ok(ProductDto.ToDto(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Category_Id = dto.Category_Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = ProductDto.ToDto(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Product_Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Category_Id = dto.Category_Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            if (dto.ProductName != null)
                product.ProductName = dto.ProductName;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            if (dto.Category_Id.HasValue)
                product.Category_Id = dto.Category_Id.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}