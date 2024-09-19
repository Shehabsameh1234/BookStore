namespace BookStore.Core.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemsDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
