namespace OmoqoTest.Contracts.Ships
{
    public record ShipUpdateRequest(int Id, string? Code, string? Name, int Width, int Length);
}