using BookStore.Core.Entities.Books;
using BookStore.Core.Helpers;


namespace BookStore.Core.Specifications.BookSpecifications
{
    public class BookWithCategorySpecifications :BaseSpecifications<Book>
    {
        public BookWithCategorySpecifications(QuerySpecParameters querySpec) :base(b => !querySpec.CategoryId.HasValue || b.CategoryId == querySpec.CategoryId.Value)
        {
            Includes.Add(b => b.Category);
            if(!string.IsNullOrEmpty(querySpec.Sort))
            {
                switch(querySpec.Sort)
                {
                    case "priceAsc":
                        AddOrderby(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderbyDesc(p=>p.Price);
                        break;
                    case "nameDesc":
                        AddOrderbyDesc(p => p.Name);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                }
            }
            else AddOrderby(p=>p.Name);

            ApplyPagination((querySpec.PageIndex-1)*querySpec.PageSize,querySpec.PageSize);
            
        }
        public BookWithCategorySpecifications(int id):base(b=>b.Id==id)
        {
            Includes.Add(b => b.Category);

        }
    }
}
