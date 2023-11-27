using ErrorOr;
using MediatR;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Delete
{
    public record ShipRemoveCommand : IRequest<ErrorOr<Ship>>
    {
        public int Id;
    }
}