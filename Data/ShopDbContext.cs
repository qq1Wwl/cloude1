using Microsoft.EntityFrameworkCore;
using pr9ASP.Models;

namespace pr9ASP.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Product_Id);
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.Category_Id).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
            });
        }
    }
}