using BookStore.Core.Entities.Basket;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket?> AddItemToBasketAsync(string basketId, int productId)
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
                        return null;
                    }
                }
                getBasket.Items.Add(item);
                await _basketRepository.UpdateBasketAsync(getBasket);
                return getBasket;

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
                if (createdOrUpdateBasket == null) return null;
                return createdOrUpdateBasket;
            }
        }

        public async Task<CustomerBasket?> DeleteItemFromBasketAsync(string basketId, int productId)
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
                        var ooo=getBasket.Items.Remove(item1);
                        await _basketRepository.UpdateBasketAsync(getBasket);
                        return getBasket;
                    }
                }
                return null;
                
            }
            else return null;

        }
    }
}
