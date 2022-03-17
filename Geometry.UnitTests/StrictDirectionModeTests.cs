using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Strict.Definitions;
using Xunit;

namespace CodeChops.Geometry.UnitTests;

public class StrictDirectionModeTests
{
	[Theory]
	[InlineData(nameof(EveryDirection.East),		nameof(RotationType.Invert),			nameof(EveryDirection.West))]
	[InlineData(nameof(EveryDirection.South),		nameof(RotationType.Clockwise),			nameof(EveryDirection.SouthWest))]
	[InlineData(nameof(EveryDirection.NorthEast),	nameof(RotationType.Invert),			nameof(EveryDirection.SouthWest))]
	[InlineData(nameof(EveryDirection.NorthEast),	nameof(RotationType.CounterClockwise),	nameof(EveryDirection.North))]
	[InlineData(nameof(EveryDirection.SouthWest),	nameof(RotationType.Clockwise),			nameof(EveryDirection.West))]
	[InlineData(nameof(EveryDirection.North),		nameof(RotationType.CounterClockwise),	nameof(EveryDirection.NorthWest))]
	public void TurnIsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = EveryDirection.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(EveryDirection.North),	nameof(OrthogonalDirection.Up))]
	[InlineData(nameof(EveryDirection.East),	nameof(OrthogonalDirection.Right))]
	[InlineData(nameof(EveryDirection.South),	nameof(OrthogonalDirection.Down))]
	[InlineData(nameof(EveryDirection.West),	nameof(OrthogonalDirection.Left))]
	public void CastDirection_ShouldWork(string directionName, string expectedDirectionName)
	{
		var deltaPoint = EveryDirection.GetSingleMember(directionName);
		var newDirectionName = EveryDirection.Cast<OrthogonalDirection>(deltaPoint).Name;

		Assert.Equal(newDirectionName, expectedDirectionName);
	}
}