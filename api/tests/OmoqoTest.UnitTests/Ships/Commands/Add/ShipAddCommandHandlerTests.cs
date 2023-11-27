using OmoqoTest.Application.Ships.Commands.Add;
using OmoqoTest.Application.Repositories;

using Moq;
using FluentAssertions;
using OmoqoTest.UnitTests.TestUtils.Ships;
using OmoqoTest.Application.UnitTests.TestUtils.Ships.Extensions;

namespace OmoqoTest.UnitTests.Ships.Commands.Add
{
    public class ShipAddCommandHandlerTests
    {
        private readonly ShipAddCommandHandler _handler;
        private readonly Mock<IShipRepository> _mockShipRepository;

        public ShipAddCommandHandlerTests()
        {
            _mockShipRepository = new Mock<IShipRepository>();
            _handler = new ShipAddCommandHandler(_mockShipRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidShipAddCommands))]
        public async Task HandleShipAddCommand_WhenShipIsValid_ShouldCreateAndReturnShip(ShipAddCommand shipAddCommand)
        {
            var result = await _handler.Handle(shipAddCommand, default);

            result.IsError.Should().BeFalse();
            result.Value.ValidateCreatedFrom(shipAddCommand);
            _mockShipRepository.Verify(m => m.AddAsync(result.Value), Times.Once);
        }

        public static IEnumerable<object[]> ValidShipAddCommands()
        {
            yield return new[] { ShipAddCommandUtils.CreateCommand() };
        }
    }
}