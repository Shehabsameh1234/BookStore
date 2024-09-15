using BookStore.Core.Entities.Books;
using BookStore.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Specifications.BookSpecifications
{
    public class BookWithCountSpecifications : BaseSpecifications<Book>
    {
        public BookWithCountSpecifications(QuerySpecParameters querySpec) : base(b => !querySpec.CategoryId.HasValue || b.CategoryId == querySpec.CategoryId.Value)
        {

        }
    }
}
