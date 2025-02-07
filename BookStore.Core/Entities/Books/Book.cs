
namespace BookStore.Core.Entities.Books
{
    public class Book:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
