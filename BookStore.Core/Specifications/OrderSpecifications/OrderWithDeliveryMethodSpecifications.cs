using BookStore.Core.Entities.Orders;


namespace BookStore.Core.Specifications.OrderSpecifications
{
    public class OrderWithDeliveryMethodSpecifications:BaseSpecifications<Order>
    {
        public OrderWithDeliveryMethodSpecifications(int id):base(o=>o.Id==id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);

        }

    }
}
