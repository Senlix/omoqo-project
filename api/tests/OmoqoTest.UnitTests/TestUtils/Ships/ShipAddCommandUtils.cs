using OmoqoTest.Application.Ships.Commands.Add;

namespace OmoqoTest.UnitTests.TestUtils.Ships
{

    public static class ShipAddCommandUtils
    {
        public static ShipAddCommand CreateCommand() => new(
            Constants.Ship.Code,
            Constants.Ship.Name,
            Constants.Ship.Width,
            Constants.Ship.Length
        );
    }
}
