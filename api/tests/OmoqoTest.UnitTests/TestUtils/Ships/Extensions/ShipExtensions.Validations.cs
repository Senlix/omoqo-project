using OmoqoTest.Application.Ships.Commands.Add;

using FluentAssertions;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.UnitTests.TestUtils.Ships.Extensions
{

    public static partial class ShipExtensions
    {
        public static void ValidateCreatedFrom(this Ship ship, ShipAddCommand command)
        {
            ship.Code.Should().NotBe(null);
            ship.Code.Should().MatchRegex(@"^[A-Za-z]{4}-[0-9]{4}-[A-Za-z]{1}[0-9]{1}$");
            ship.Code.Should().Be(command.Code);
            
            ship.Name.Should().NotBe(null);
            ship.Name.Should().Be(command.Name);
            
            ship.Width.Should().NotBe(null);
            ship.Width.Should().BeLessThanOrEqualTo(100);
            ship.Width.Should().BeGreaterThanOrEqualTo(10);
            ship.Width.Should().Be(command.Width);
            
            ship.Length.Should().NotBe(null);
            ship.Length.Should().BeLessThanOrEqualTo(500);
            ship.Length.Should().BeGreaterThanOrEqualTo(50);
            ship.Length.Should().Be(command.Length);

            ship.IsValid.Should().Be(true);
        }
    }
}
