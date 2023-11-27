using ErrorOr;
using MediatR;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Queries.Get
{
    public class ShipGetQueryHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipGetQuery, ErrorOr<Ship>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        public async Task<ErrorOr<Ship>> Handle(ShipGetQuery request, CancellationToken cancellationToken)
        {
            if (await _shipRepository.GetByIdAsync(request.Id) is not Ship ship)
            {
                return Errors.Ship.NotFound;
            }

            return ship;
        }

    }
}