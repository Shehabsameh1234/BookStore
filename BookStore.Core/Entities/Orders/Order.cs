

namespace BookStore.Core.Entities.Orders
{
    public class Order:BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderAddress OrderAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        


    }
}
