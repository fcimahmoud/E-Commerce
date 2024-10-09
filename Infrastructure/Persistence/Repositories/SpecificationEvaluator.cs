
namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> query, 
            Specifications<T> specifications)
            where T : class
        {
            if (specifications.Criteria is not null) 
                query = query.Where(specifications.Criteria);

            /*            foreach (var item in specifications.IncludeExpression)
                            query = query.Include(item);*/
            
            query = specifications.IncludeExpression.Aggregate(query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if(specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);


            return query;
        }
    }
}