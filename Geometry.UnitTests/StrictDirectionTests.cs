using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Space.Directions.Strict.Modes;

namespace CodeChops.Geometry.UnitTests;

public class StrictDirectionTests
{
	[Theory]
	[InlineData(nameof(EveryDirection<int>.East),		nameof(RotationType.Invert),			nameof(EveryDirection<int>.West))]
	[InlineData(nameof(EveryDirection<int>.South),		nameof(RotationType.Clockwise),			nameof(EveryDirection<int>.SouthWest))]
	[InlineData(nameof(EveryDirection<int>.NorthEast),	nameof(RotationType.Invert),			nameof(EveryDirection<int>.SouthWest))]
	[InlineData(nameof(EveryDirection<int>.NorthEast),	nameof(RotationType.CounterClockwise),	nameof(EveryDirection<int>.North))]
	[InlineData(nameof(EveryDirection<int>.SouthWest),	nameof(RotationType.Clockwise),			nameof(EveryDirection<int>.West))]
	[InlineData(nameof(EveryDirection<int>.North),		nameof(RotationType.CounterClockwise),	nameof(EveryDirection<int>.NorthWest))]
	public void EveryDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = EveryDirection<int>.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(OrthogonalDirection<int>.Right),	nameof(RotationType.Invert),			nameof(OrthogonalDirection<int>.Left))]
	[InlineData(nameof(OrthogonalDirection<int>.Down),	nameof(RotationType.Clockwise),			nameof(OrthogonalDirection<int>.Left))]
	[InlineData(nameof(OrthogonalDirection<int>.Up),	nameof(RotationType.Invert),			nameof(OrthogonalDirection<int>.Down))]
	[InlineData(nameof(OrthogonalDirection<int>.Right),	nameof(RotationType.CounterClockwise),	nameof(OrthogonalDirection<int>.Up))]
	public void OrthogonalDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = OrthogonalDirection<int>.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(EveryDirection<int>.North),	nameof(OrthogonalDirection<int>.Up))]
	[InlineData(nameof(EveryDirection<int>.East),	nameof(OrthogonalDirection<int>.Right))]
	[InlineData(nameof(EveryDirection<int>.South),	nameof(OrthogonalDirection<int>.Down))]
	[InlineData(nameof(EveryDirection<int>.West),	nameof(OrthogonalDirection<int>.Left))]
	public void Direction_Cast_ShouldWork(string directionName, string expectedDirectionName)
	{
		var deltaPoint = EveryDirection<int>.GetSingleMember(directionName);
		var newDirectionName = EveryDirection<int>.Cast<OrthogonalDirection<int>, int>(deltaPoint).Name;

		Assert.Equal(newDirectionName, expectedDirectionName);
	}
}