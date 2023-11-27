using ErrorOr;
using MediatR;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Update
{
    public record ShipUpdateCommand : IRequest<ErrorOr<Ship>>
    {

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
    }
}