using BookStore.Core.Entities;
using BookStore.Core.Repository.Contract;

namespace BookStore.Core.IUnitOfWork
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
    }
}
