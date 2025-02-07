using BookStore.Core.Entities.Orders;


namespace BookStore.Core.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrederDate { get; set; }
        public string OrderStatus { get; set; } = null!;
        public OrderAddress OrderAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public ICollection<OrderItems> OrderItems { get; set; } = new HashSet<OrderItems>();
        public decimal SubTotal { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public decimal Total { get; set; }
        
    }
}
