using AutoMapper;
using BookStore.Api.Errors;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Orders;
using BookStore.Core.Helpers;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using static System.Net.WebRequestMethods;


namespace BookStore.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : ApiBaseController
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IOptions<StripeSettings> stripeSettings, IOrderService orderService,IMapper mapper,IPaymentService paymentService)
        {
            _stripeSettings = stripeSettings.Value;
            _orderService = orderService;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<ActionResult> CreateCheckOutSession(int orderId, string successUrl,string cancelUrl)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetUserOrder(email, orderId);
            if (order == null) return NotFound(new ApisResponse(404));
            if (order.OrderStatus == OrderStatus.PaymentRecieved) return BadRequest(new ApisResponse(400, "Order Already Paid"));
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
            
            #region payment
            var currency = "Usd";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                   new SessionLineItemOptions
                   {
                       PriceData = new SessionLineItemPriceDataOptions
                       {
                           Currency=currency,
                           UnitAmount=Convert.ToInt32(mappedOrder.Total)*100,
                           ProductData = new SessionLineItemPriceDataProductDataOptions
                           {
                               Name = "Book Store",
                               Description="Pay For Order"
                           },
                       },
                       Quantity=1
                   }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                CustomerEmail = mappedOrder.BuyerEmail,
            };
            var service = new SessionService();
            var session = await  service.CreateAsync(options);
            return Ok(session);
            #endregion
        }
        [HttpPost("sendEmail")]
        public async Task<ActionResult> SendEmailToUser(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
             _paymentService.SendEmailToCustomer(orderId, email);
            return Ok();
        }
    }
}
