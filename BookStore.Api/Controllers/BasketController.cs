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
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket)
        {
            var getBasket = await _basketRepository.GetBasketById(basket.Id);
            if (getBasket != null)
            {
                foreach (var item in basket.Items)
                {
                   getBasket.Items.Add(item);
                }
                await _basketRepository.UpdateBasketAsync(getBasket);
                return Ok(getBasket);
            }
            else
            {
                var createdOrUpdateBasket = await _basketRepository.UpdateBasketAsync(basket);
                if (createdOrUpdateBasket == null) return BadRequest(new ApisResponse(400));
                return Ok(createdOrUpdateBasket);
            }
        }

    }
}
