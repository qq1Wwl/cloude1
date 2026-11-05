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

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(x => x.Role_Id);
                e.Property(x => x.RoleName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(x => x.User_Id);
                e.Property(x => x.Login).IsRequired().HasMaxLength(50);
                e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(x => x.Category_Id);
                e.Property(x => x.CategoryName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(x => x.Product_Id);
                e.Property(x => x.ProductName).IsRequired().HasMaxLength(200);
                e.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            });

            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("Carts");
                e.HasKey(x => x.Cart_Id);
            });

            modelBuilder.Entity<CartItem>(e =>
            {
                e.ToTable("CartItems");
                e.HasKey(x => x.CartItem_Id);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(x => x.Order_Id);
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.ToTable("OrderItems");
                e.HasKey(x => x.OrderItem_Id);
                e.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            });
        }
    }
}