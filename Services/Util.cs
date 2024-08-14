using MTG_Project.Models;
using System.Linq.Expressions;

namespace MTG_Project.Services
{
    public class Util
    {
        public static IQueryable<Card> OrderBySortOrder(IQueryable<Card> query, string sortOrder)
        {
            var parameter = Expression.Parameter(typeof(Card), "c");
            var property = Expression.Property(parameter, sortOrder);
            var lambda = Expression.Lambda(property, parameter);

            var orderByMethod = typeof(Queryable).GetMethods()
                .First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(Card), property.Type);

            return (IQueryable<Card>)orderByMethod.Invoke(null, parameters: [query, lambda]);
        }

        public static IQueryable<Card> Paginate(IQueryable<Card> query, int pageNumber, int pageSize)
        {
            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
