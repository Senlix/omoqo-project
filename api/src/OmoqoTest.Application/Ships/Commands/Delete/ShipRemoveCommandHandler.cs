using ErrorOr;
using MediatR;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Delete
{
    public class ShipRemoveCommandHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipRemoveCommand, ErrorOr<Ship>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        public async Task<ErrorOr<Ship>> Handle(ShipRemoveCommand request, CancellationToken cancellationToken)
        {
            if (await _shipRepository.GetByIdAsync(request.Id) is not Ship ship)
            {
                return Errors.Ship.NotFound;
            }

            await _shipRepository.RemoveAsync(ship);
            return ship;
        }

    }
}