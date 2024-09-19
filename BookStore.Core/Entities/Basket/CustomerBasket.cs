

namespace BookStore.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItems> Items { get; set; }
        public decimal TotalAmount { get; set; }
    } 
}
