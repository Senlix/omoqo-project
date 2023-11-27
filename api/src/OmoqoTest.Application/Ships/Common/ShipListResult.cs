namespace OmoqoTest.Application.Ships.Common
{
    public record ShipListResult(
        int Id,
        string Code,
        string Name,
        int Width,
        int Length
    );
}