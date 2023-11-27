using ErrorOr;
using MediatR;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Add
{
    public class ShipAddCommandHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipAddCommand, ErrorOr<Ship>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        public async Task<ErrorOr<Ship>> Handle(ShipAddCommand request, CancellationToken cancellationToken)
        {
            Ship ship = new(request.Code, request.Name, request.Width, request.Length);

            if (!ship.IsValid) return ship.ErrorsList;

            await _shipRepository.AddAsync(ship);

            return ship;
        }

    }
}