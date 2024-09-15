using BookStore.Core.Entities;
using BookStore.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query,ISpecifications<T> spec)
        {
            var newquery = query;

            if(spec.Criteria is not null)
              newquery= newquery.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                newquery = newquery.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)
                newquery = newquery.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPaginationEnabled ==true)
                newquery = newquery.Skip(spec.Skip).Take(spec.Take);

            newquery = spec.Includes.Aggregate(newquery, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return newquery;
        }
    }
}
