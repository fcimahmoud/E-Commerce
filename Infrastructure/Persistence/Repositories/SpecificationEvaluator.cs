
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

            return query;
        }
    }
}