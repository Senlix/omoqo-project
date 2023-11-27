using ErrorOr;
using MediatR;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Update
{
    public class ShipUpdateCommandHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipUpdateCommand, ErrorOr<Ship>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        public async Task<ErrorOr<Ship>> Handle(ShipUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _shipRepository.GetByIdAsync(request.Id) is not Ship ship)
            {
                return Errors.Ship.NotFound;
            }

            ship.Update(
                ship.Code = request.Code,
                ship.Name = request.Name,
                ship.Width = request.Width,
                ship.Length = request.Length);

            if (!ship.IsValid) return ship.ErrorsList;

            await _shipRepository.UpdateAsync(ship);

            return ship;
        }

    }
}