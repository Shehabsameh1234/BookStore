using AutoMapper;
using BookStore.Api.Errors;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Orders;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var mappedAddress = _mapper.Map<OrderAddress>(orderDto.OrderAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BasketId, email, mappedAddress, orderDto.DeliveryMethodId);
            if (order == null) return BadRequest(new ApisResponse(400));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);

            return Ok(mappedOrder);
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<ActionResult<OrderToReturnDto>> UpdateOrderStatus(int orderId)
        {
            var order = await _orderService.UpdateOrderSatus(orderId);
            if (order == null) return NotFound(new ApisResponse(404));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
            return Ok(mappedOrder);

        }

        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetUserOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetUserOrders(email);
            if (orders == null) return NotFound(new ApisResponse(404));
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(mappedOrder);
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> GetUserOrder(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetUserOrder(email,orderId);
            if (order == null) return NotFound(new ApisResponse(404));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
            return Ok(mappedOrder);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = new
            {
                DeleteStatus = await _orderService.DeleteOrder(email, orderId) ? "deleted" : "order not found"
            };
            return Ok(result);
        }
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethod =await _orderService.GetDeliverymethodsAsync();
            return Ok(deliveryMethod);
        }


    }
}
