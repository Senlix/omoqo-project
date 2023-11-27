using ErrorOr;
using MediatR;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Delete
{
    public class ShipBulkRemoveCommandHandler(
        IShipRepository shipRepository
    ) : IRequestHandler<ShipBulkRemoveCommand, ErrorOr<bool>>
    {
        private readonly IShipRepository _shipRepository = shipRepository;

        public async Task<ErrorOr<bool>> Handle(ShipBulkRemoveCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request);
            List<Ship> shipList = [];

            foreach (var id in request.Ids)
            {
                Ship? ship = await _shipRepository.GetByIdAsync(id);

                if (ship is null)
                {
                    return Errors.Ship.NotFound;
                }

                shipList.Add(ship);
            }

            shipList.ForEach(async ship => await _shipRepository.RemoveAsync(ship));
            return true;
        }
    }
}