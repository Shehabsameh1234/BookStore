

namespace BookStore.Core.Entities.Basket
{
    public class BasketItems
    {
        public int Id { get; set; }
        public int Quantity { get; set;}
        public int InStock { get; set; }
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return Quantity * Price; }
           
        }
    }
}
