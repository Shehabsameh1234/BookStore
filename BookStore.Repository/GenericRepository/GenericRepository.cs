using BookStore.Core.Entities;
using BookStore.Core.Entities.Books;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Specifications;
using BookStore.Repository.ConText;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _storeContext.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
        => await _storeContext.Set<T>().FindAsync(id);
        
        public void Add(T entity)
        => _storeContext.Add(entity);

        public void Delete(T entity)
        => _storeContext.Remove(entity);

        public void Update(T entity)
        => _storeContext.Update(entity);

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
        =>await SpecificationsEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec).FirstOrDefaultAsync();
       

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        => await SpecificationsEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec).ToListAsync();

        public async Task<IReadOnlyList<Book>> GetAllAsyncBt3ty()
        =>  await _storeContext.Set<Book>().Include(b=>b.Category).Where(b=>b.CategoryId==6).Skip(4).Take(2).ToListAsync();

        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        => await SpecificationsEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec).CountAsync();

    }
}
