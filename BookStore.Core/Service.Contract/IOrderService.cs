using BookStore.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Service.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string basketId,string buyerEmail , OrderAddress orderAddress,int deliveryMethodId);
    }
}
