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
        private readonly IBooksService _booksService;

        public BasketService(IBasketRepository basketRepository,IBooksService booksService)
        {
            _basketRepository = basketRepository;
            _booksService = booksService;
        }
        public async Task<CustomerBasket?> AddItemToBasketAsync(string basketId, int bookiD)
        {
            var book = await _booksService.GetBookAsync(bookiD);
            if (book == null) return null;
            var item = new BasketItems()
            {
                ProductId = bookiD,
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
            var getBasket = await _basketRepository.GetBasketById(basketId);
            if (getBasket != null)
            {
                foreach (var item1 in getBasket.Items)
                {
                    if (item1.ProductId == productId)
                    {
                        getBasket.Items.Remove(item1);
                        await _basketRepository.UpdateBasketAsync(getBasket);
                        return getBasket;
                    }
                }
                return null;
                
            }
            else return null;

        }

        public async Task<CustomerBasket?> UpdateItemQuantityAsync(string basketId, int productId, int quantity)
        {
            var getBasket = await _basketRepository.GetBasketById(basketId);
            if (getBasket != null)
            {
                foreach (var item1 in getBasket.Items)
                {
                    if (item1.ProductId == productId)
                    {
                        item1.Quantity = quantity;
                        await _basketRepository.UpdateBasketAsync(getBasket);
                        return getBasket;
                    }
                    else return null;
                }
                return null;
            }
            else return null;
        }
    }
}
