
namespace BookStore.Core.Service.Contract
{
    public interface ICashingService
    {
        Task CashResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string?> GetCashResponseAsync(string key);
    }
}
