using BookStore.Core.Entities.Orders;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
 
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        //public async Task<ActionResult<Order>> CreateOrder(OrderDto order)
    }
}
