using System.Linq.Expressions;
using System.Reflection;

namespace OmoqoTest.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderByDynamic<TEntity>(
            this IQueryable<TEntity> source,
            List<OrderByExpression<TEntity>> orderExpressions)
        {
            IOrderedQueryable<TEntity>? orderedQuery = null;

            foreach (var orderExpression in orderExpressions)
            {
                if (orderExpression.OrderType == OrderByType.Ascending)
                {
                    orderedQuery = orderedQuery?.ThenBy(orderExpression.Expression) ??
                                   source.OrderBy(orderExpression.Expression);
                }
                else
                {
                    orderedQuery = orderedQuery?.ThenByDescending(orderExpression.Expression) ??
                                   source.OrderByDescending(orderExpression.Expression);
                }
            }

            return orderedQuery ?? (IOrderedQueryable<TEntity>)source;
        }

        public static List<OrderByExpression<TEntity>> ParseOrderBy<TEntity>(string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return [];
            }

            var orderExpressions = new List<OrderByExpression<TEntity>>();

            var orderPairs = orderBy.Split(',');

            foreach (var orderPair in orderPairs)
            {
                var parts = orderPair.Trim().Split(':');

                if (parts.Length > 0)
                {
                    var propertyName = parts[0];

                    if (!IsPropertyValid<TEntity>(propertyName))
                    {
                        throw new ArgumentException($"Property {propertyName} is not valid for the entity {typeof(TEntity).Name}.");
                    }

                    var orderType = OrderByType.Ascending;
                    if (parts.Length > 1 && parts[1].Equals("descend", StringComparison.OrdinalIgnoreCase))
                    {
                        orderType = OrderByType.Descending;
                    }

                    var parameter = Expression.Parameter(typeof(TEntity));
                    var property = Expression.Property(parameter, propertyName);
                    var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

                    orderExpressions.Add(new OrderByExpression<TEntity>
                    {
                        Expression = lambda,
                        OrderType = orderType
                    });
                }
            }

            return orderExpressions;
        }

        private static bool IsPropertyValid<TEntity>(string propertyName)
        {
            Type entityType = typeof(TEntity);
            return entityType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) != null;
        }

    }

    public class OrderByExpression<T>
    {
        public required Expression<Func<T, object>> Expression { get; set; }
        public OrderByType OrderType { get; set; }
    }

    public enum OrderByType
    {
        Ascending,
        Descending
    }

}