using BookStore.Core.Service.Contract;
using StackExchange.Redis;
using System.Text.Json;


namespace BookStore.Service.CashingService
{
    public class CashingService : ICashingService
    {
        private readonly IDatabase _dataBase;
        public CashingService(IConnectionMultiplexer redis) 
        {
            _dataBase= redis.GetDatabase();
        }

        public async Task CashResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response == null) return;

            //make response camel case
            var jsonCamelCase = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            //convert response to json
            var serialezedResponse = JsonSerializer.Serialize(response,jsonCamelCase);

            //save response to redis dataBase
            await _dataBase.StringSetAsync(key,serialezedResponse,timeToLive);

        }

        public async Task<string?> GetCashResponseAsync(string key)
        {
            //get data from redis database using key
            var response = await _dataBase.StringGetAsync(key);

            if(response.IsNullOrEmpty) return null;

            //return data
            return response;
        }
    }
}
