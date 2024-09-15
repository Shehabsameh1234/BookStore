using BookStore.Core.Entities.Books;
using BookStore.Core.Helpers;


namespace BookStore.Core.Service.Contract
{
    public interface IBooksService
    {
        Task<IReadOnlyList<Book>> GetAllBooksAsync(QuerySpecParameters querySpec);
        Task<IReadOnlyList<Category>> GetAllCategoriesAsync();
        Task<Book?> GetBookAsync(int id);
        Task<int> GetCountAsync(QuerySpecParameters querySpec);
        
    }
}
