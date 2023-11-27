using ErrorOr;
using MediatR;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Commands.Add
{
    public record ShipAddCommand(string? Code, string? Name, int Width, int Length) : IRequest<ErrorOr<Ship>>
    {
        public string? Code { get; set; } = Code;
        public string? Name { get; set; } = Name;
        public int Width { get; set; } = Width;
        public int Length { get; set; } = Length;
    }
}