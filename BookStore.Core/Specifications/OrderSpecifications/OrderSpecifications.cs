using BookStore.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email) : base(o => o.BuyerEmail == email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);

        }
        public OrderSpecifications(string email, int orderId) : base(o => o.BuyerEmail == email && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);

        }
    }
}
