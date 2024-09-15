using BookStore.Core.Entities;
using BookStore.Core.IUnitOfWork;
using BookStore.Core.Repository.Contract;
using BookStore.Repository.ConText;
using BookStore.Repository.GenericRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly Hashtable _repository;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repository = new Hashtable();
            
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            //get the name of t entity like (product)
            var key = typeof(T).Name;
            if (!_repository.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_storeContext);
                _repository.Add(key, repository);
            }
            return _repository[key] as IGenericRepository<T>;
        }
        public async Task<int> CompleteAsync()
       =>await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _storeContext.DisposeAsync();
    }
}
