using BookStore.Core.Entities;
using System.Linq.Expressions;


namespace BookStore.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        Expression<Func<T,bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
        Expression<Func<T,object>> OrderByDesc { get; set; }
        Expression<Func<T, object>> OrderBy { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsPaginationEnabled { get; set; }

    }
}
