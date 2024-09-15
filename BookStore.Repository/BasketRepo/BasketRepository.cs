using BookStore.Core.Entities.Basket;
using BookStore.Core.Repository.Contract;
using StackExchange.Redis;
using System.Text.Json;

namespace BookStore.Repository.BasketRepo
{
    public class BasketRepository :IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketById(string basketId)
        {
            var basket =await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
           var isCreatedOrUpdated= await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if(!isCreatedOrUpdated) return null;
            return await  GetBasketById(basket.Id);
        }
    }
}
