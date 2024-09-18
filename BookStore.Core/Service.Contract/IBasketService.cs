using BookStore.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Service.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasket?> AddItemToBasketAsync(string basketId,int productId);
    }
}
