using BookStore.Api.Errors;
using BookStore.Core.Entities.Basket;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketService _basketService;

        public BasketController(IBasketRepository basketRepository,IBasketService basketService)
        {
            _basketRepository = basketRepository;
            _basketService = basketService;
        }
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> AddItemToBasket(string basketId,int productId )
        {
            var basket =await  _basketService.AddItemToBasketAsync(basketId, productId);
            if (basket == null) return BadRequest(new ApisResponse(400, "item Already in basket"));
            return Ok(basket);
       
        }
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("deleteItem")]
        public async Task<ActionResult<CustomerBasket>> DeleteItemFromBasket(string basketId, int productId)
        {
            var basket = await _basketService.DeleteItemFromBasketAsync(basketId,productId);
            if (basket == null) return NotFound(new ApisResponse(404, "product or basket not found"));
            return Ok(basket);
        }
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<ActionResult<CustomerBasket>> UpdateItemQuantity(string basketId, int productId,int quantity)
        {
            var basket = await _basketService.UpdateItemQuantityAsync(basketId, productId,quantity);
            if(basket==null) return NotFound(new ApisResponse(404, "product or basket not found"));
            return Ok(basket);
        }

    }
}
