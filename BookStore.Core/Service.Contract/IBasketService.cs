using BookStore.Core.Entities.Basket;


namespace BookStore.Core.Service.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasket?> AddItemToBasketAsync(string basketId,int productId);
        Task<CustomerBasket?> DeleteItemFromBasketAsync(string basketId, int productId);
        Task<CustomerBasket?> DeleteAllItemsFromBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateItemQuantityAsync(string basketId, int productId,int quantity);

    }
}
