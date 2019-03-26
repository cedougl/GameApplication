using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameApi.Controllers
{
    /// <summary>
    /// Extension class used for sorting by column name in a specific order
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Order enum - Ascending or Descending
        /// </summary>
        public enum Order
        {
            Asc,
            Desc
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="query">IQueryable list to be sorted</param>
        /// <param name="orderByMember">Column name to sort by</param>
        /// <param name="direction">Order enum - Ascending or descending</param>
        /// <returns>IQueryable list in sorted order according to parameters</returns>
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            Order direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == Order.Asc ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }
    }
}