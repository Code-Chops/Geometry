using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Space.Directions.Strict.Modes;

namespace CodeChops.Geometry.UnitTests;

public class StrictDirectionTests
{
	[Theory]
	[InlineData(nameof(EveryDirectionMode<int>.East),		nameof(RotationType.Invert),			nameof(EveryDirectionMode<int>.West))]
	[InlineData(nameof(EveryDirectionMode<int>.South),		nameof(RotationType.Clockwise),			nameof(EveryDirectionMode<int>.SouthWest))]
	[InlineData(nameof(EveryDirectionMode<int>.NorthEast),	nameof(RotationType.Invert),			nameof(EveryDirectionMode<int>.SouthWest))]
	[InlineData(nameof(EveryDirectionMode<int>.NorthEast),	nameof(RotationType.CounterClockwise),	nameof(EveryDirectionMode<int>.North))]
	[InlineData(nameof(EveryDirectionMode<int>.SouthWest),	nameof(RotationType.Clockwise),			nameof(EveryDirectionMode<int>.West))]
	[InlineData(nameof(EveryDirectionMode<int>.North),		nameof(RotationType.CounterClockwise),	nameof(EveryDirectionMode<int>.NorthWest))]
	public void EveryDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = EveryDirectionMode<int>.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(OrthogonalDirectionMode<int>.Right),	nameof(RotationType.Invert),			nameof(OrthogonalDirectionMode<int>.Left))]
	[InlineData(nameof(OrthogonalDirectionMode<int>.Down),	nameof(RotationType.Clockwise),			nameof(OrthogonalDirectionMode<int>.Left))]
	[InlineData(nameof(OrthogonalDirectionMode<int>.Up),	nameof(RotationType.Invert),			nameof(OrthogonalDirectionMode<int>.Down))]
	[InlineData(nameof(OrthogonalDirectionMode<int>.Right),	nameof(RotationType.CounterClockwise),	nameof(OrthogonalDirectionMode<int>.Up))]
	public void OrthogonalDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = OrthogonalDirectionMode<int>.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(EveryDirectionMode<int>.North),	nameof(OrthogonalDirectionMode<int>.Up))]
	[InlineData(nameof(EveryDirectionMode<int>.East),	nameof(OrthogonalDirectionMode<int>.Right))]
	[InlineData(nameof(EveryDirectionMode<int>.South),	nameof(OrthogonalDirectionMode<int>.Down))]
	[InlineData(nameof(EveryDirectionMode<int>.West),	nameof(OrthogonalDirectionMode<int>.Left))]
	public void Direction_Cast_ShouldWork(string directionName, string expectedDirectionName)
	{
		var deltaPoint = EveryDirectionMode<int>.GetSingleMember(directionName);
		var newDirectionName = EveryDirectionMode<int>.Cast<OrthogonalDirectionMode<int>, int>(deltaPoint).Name;

		Assert.Equal(newDirectionName, expectedDirectionName);
	}
}