using AutoMapper;
using BookStore.Api.Errors;
using BookStore.Core.Dtos;
using BookStore.Core.Entities;
using BookStore.Core.Entities.Orders;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var mappedAddress = _mapper.Map<OrderAddress>(orderDto.OrderAddress);
            var order = await  _orderService.CreateOrderAsync(orderDto.BasketId, email, mappedAddress, orderDto.DeliveryMethodId);
            if (order == null) return BadRequest(new ApisResponse(400));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);

            return Ok(mappedOrder);
        }

        [HttpPut]
        public async Task<ActionResult<OrderToReturnDto>> UpdateOrderStatus(int orderId)
        {
            var order =await _orderService.UpdateOrderSatus(orderId);
            if (order == null) return NotFound(new ApisResponse(404));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
            return Ok(mappedOrder);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetUserOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders =await  _orderService.GetUserOrders(email);
            if (orders == null) return NotFound(new ApisResponse(404));
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(mappedOrder);
        }


    }
}
