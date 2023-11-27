using ErrorOr;
using MediatR;

namespace OmoqoTest.Application.Ships.Commands.Delete
{
    public record ShipBulkRemoveCommand : IRequest<ErrorOr<bool>>
    {
        public List<int> Ids = [];
    }
}