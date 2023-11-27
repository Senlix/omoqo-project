using ErrorOr;
using MediatR;
using OmoqoTest.Domain.Common;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Ships.Queries.List
{
    public record ShipListQuery : IRequest<ErrorOr<PaginatedList<Ship>>>
    {
        public string? Code { get; set; } = null;
        public string? Name { get; set; } = null;
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 50;
        public string? OrderBy { get; set; } = null!;
    }
}