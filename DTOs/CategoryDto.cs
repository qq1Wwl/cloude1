using pr9ASP.Models;

namespace pr9ASP.DTOs
{
    public class CategoryDto
    {
        public int Category_Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static CategoryDto ToDto(Category category) => new CategoryDto
        {
            Category_Id = category.Category_Id,
            CategoryName = category.CategoryName,
            CreatedAt = category.CreatedAt
        };
    }
}