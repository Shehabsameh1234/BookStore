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
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
 
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    }
}
