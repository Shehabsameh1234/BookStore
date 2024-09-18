using BookStore.Api.Errors;
using BookStore.Core.Entities.Basket;
using BookStore.Core.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketById(id);
            if(basket == null) return NotFound(new ApisResponse(404));
            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string basketId)
        {
            var result = new
            {
                DeleteStatus = await _basketRepository.DeleteBasketAsync(basketId) ? "deleted" : "basket not found"
            };
           return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> AddItemToBasket(string basketId,int productId )
        {
            var item = new BasketItems()
            {
                ProductId = productId,
                Quantity = 1,
            };
            var getBasket = await _basketRepository.GetBasketById(basketId);
            if (getBasket != null)
            {
              
                foreach (var item1 in getBasket.Items)
                {
                    if (item1.ProductId == item.ProductId)
                    {
                        return BadRequest(new ApisResponse(400,"item Already in basket"));
                    }
                }
                getBasket.Items.Add(item);
                await _basketRepository.UpdateBasketAsync(getBasket);
                return Ok(getBasket);

            }
            else
            {
                List<BasketItems> basketItems = new List<BasketItems>();
                basketItems.Add(item);
                var customerBasket = new CustomerBasket()
                {
                    Id = basketId,
                    Items = basketItems
                };
                var createdOrUpdateBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
                if (createdOrUpdateBasket == null) return BadRequest(new ApisResponse(400));
                return Ok(createdOrUpdateBasket);
            }
        }

    }
}
