using ErrorOr;
using MediatR;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Queries.Get
{
    public record ShipGetQuery : IRequest<ErrorOr<Ship>>
    {
        public int Id;
    }
}