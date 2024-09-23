using BookStore.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace BookStore.Api.Controllers
{
  
    public class PaymentController : ApiBaseController
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }
        [HttpGet]
        public async Task<ActionResult> CreateCheckOutSession(string amount)
        {
            var currency = "Usd";
            var successUrl = "https://localhost:7185/api/Books";
            var cancelUrl = "https://localhost:7185/api/Books";
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
                           UnitAmount=Convert.ToInt32(amount)*100,
                           ProductData = new SessionLineItemPriceDataProductDataOptions
                           {
                               Name = "bookStore",
                               Description="bookStoreDesc"
                           }

                       },
                    Quantity=1
                   }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };
            var service = new SessionService();
            var session = service.Create(options);
            return Ok(session.Url);  
        }
    }
}
