using BookStore.Core.Entities;
using BookStore.Core.Entities.Books;
using BookStore.Core.Specifications;


namespace BookStore.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<Book>> GetAllAsyncBt3ty();
        Task<int> GetCountAsync(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}