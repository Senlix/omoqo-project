using System.Linq.Expressions;
using ErrorOr;
using MediatR;
using OmoqoTest.Application.Extensions;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Queries.List
{
    public class ShipListQueryHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipListQuery, ErrorOr<PaginatedList<Ship>>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        async Task<ErrorOr<PaginatedList<Ship>>> IRequestHandler<ShipListQuery, ErrorOr<PaginatedList<Ship>>>.Handle(ShipListQuery query, CancellationToken cancellationToken)
        {

            Expression<Func<Ship, bool>> filter = q =>
                (string.IsNullOrEmpty(query.Name) || (!string.IsNullOrEmpty(q.Name) && q.Name.Contains(query.Name, StringComparison.CurrentCultureIgnoreCase)))
                && (string.IsNullOrEmpty(query.Code) || (!string.IsNullOrEmpty(q.Code) && q.Code.Equals(query.Code, StringComparison.CurrentCultureIgnoreCase)));


            List<OrderByExpression<Ship>> orderExpressions = [];

            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                orderExpressions = QueryableExtensions.ParseOrderBy<Ship>(query.OrderBy);
            }

            var shipList = await _shipRepository.GetAllAsync(query.Page, query.Limit, filter, orderExpressions);

            return shipList;
        }

    }
}