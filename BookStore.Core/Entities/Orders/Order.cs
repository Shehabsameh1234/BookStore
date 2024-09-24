

namespace BookStore.Core.Entities.Orders
{
    public class Order:BaseEntity
    {
        private Order()
        {
            
        }
        public Order(string buyerEmail, OrderAddress orderAddress, DeliveryMethod? deliveryMethod,
           ICollection<OrderItems> orderItem, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            OrderAddress = orderAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItem;
            SubTotal = subTotal;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderAddress OrderAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => DeliveryMethod.Cost + SubTotal;

    }
}
