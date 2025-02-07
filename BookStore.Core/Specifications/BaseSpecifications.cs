using BookStore.Core.Entities;
using System.Linq.Expressions;


namespace BookStore.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public int Skip { get; set ; }
        public int Take { get ; set ; }
        public bool IsPaginationEnabled { get; set ; } =false;

        public BaseSpecifications(Expression<Func<T, bool>> criteria,int skip , int take)
        {
            Criteria = criteria;

        }
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddOrderby(Expression<Func<T, object>> orderBy)
        {
            OrderBy= orderBy;
        }
        public void AddOrderbyDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }
        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }


    }
}
