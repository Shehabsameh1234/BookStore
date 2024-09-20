using BookStore.Core.Entities.Basket;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Service.Contract;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public BasketService(IBasketRepository basketRepository,IBooksService booksService,IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _booksService = booksService;
            _configuration = configuration;
        }
        public async Task<CustomerBasket?> AddItemToBasketAsync(string basketId, int bookiD)
        {
            var book = await _booksService.GetBookAsync(bookiD);
            if (book == null || book.InStock<1) return null;
            var item = new BasketItems()
            {
                Id = bookiD,
                Quantity = 1,
                Price = book.Price,
                PictureUrl = $"{ _configuration["AppUrl"]}/{book.PictureUrl}",
                Name= book.Name,
                Author= book.Author,
                InStock=book.InStock,
            };
            var getBasket = await _basketRepository.GetBasketById(basketId);
            if (getBasket != null)
            {

                foreach (var item1 in getBasket.Items)
                {
                    if (item1.Id == item.Id)
                    {
                        return null;
                    }
                }
                getBasket.Items.Add(item);
                getBasket.TotalAmount = getBasket.Items.Sum(b => b.TotalPrice);
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
                    Items = basketItems,
                    TotalAmount= basketItems.Sum(b=>b.TotalPrice)
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
                    if (item1.Id == productId)
                    {
                        getBasket.Items.Remove(item1);
                        getBasket.TotalAmount = getBasket.Items.Sum(b => b.TotalPrice);
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
                    if (item1.Id == productId)
                    {
                        item1.Quantity = quantity;
                        getBasket.TotalAmount = getBasket.Items.Sum(b => b.TotalPrice);
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
