using BookStore.Core.Entities.Books;
using BookStore.Core.Helpers;
using BookStore.Core.IUnitOfWork;
using BookStore.Core.Service.Contract;
using BookStore.Core.Specifications.BookSpecifications;


namespace BookStore.Service.BooksService
{
    public class BooksService: IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Book>> GetAllBooksAsync(QuerySpecParameters querySpec)
        {
            var spec = new BookWithCategorySpecifications(querySpec);
            return await _unitOfWork.Repository<Book>().GetAllWithSpecAsync(spec);
          
        }
        public async Task<Book?> GetBookAsync(int id)
        {
            var spec = new BookWithCategorySpecifications(id);
            return await _unitOfWork.Repository<Book>().GetByIdWithSpecAsync(spec);
        }
        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        => await _unitOfWork.Repository<Category>().GetAllAsync();

        public async Task<int> GetCountAsync(QuerySpecParameters querySpec)
        {
            var spec = new BookWithCountSpecifications(querySpec);
            return await _unitOfWork.Repository<Book>().GetCountAsync(spec);
        }
    }
}
