using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;
using CodeChops.GenericMath.Serialization.Json;
using CodeChops.Geometry.Space;
using CodeChops.MagicEnums.Json;

namespace CodeChops.Geometry.UnitTests;

public class StrictDirectionTests
{
	static StrictDirectionTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new MagicEnumJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new IdentityJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());
	}
	
	[Theory]
	[InlineData(nameof(EveryDirection.East),		nameof(RotationType.Invert),			nameof(EveryDirection.West))]
	[InlineData(nameof(EveryDirection.South),		nameof(RotationType.Clockwise),			nameof(EveryDirection.SouthWest))]
	[InlineData(nameof(EveryDirection.NorthEast),	nameof(RotationType.Invert),			nameof(EveryDirection.SouthWest))]
	[InlineData(nameof(EveryDirection.NorthEast),	nameof(RotationType.CounterClockwise),	nameof(EveryDirection.North))]
	[InlineData(nameof(EveryDirection.SouthWest),	nameof(RotationType.Clockwise),			nameof(EveryDirection.West))]
	[InlineData(nameof(EveryDirection.North),		nameof(RotationType.CounterClockwise),	nameof(EveryDirection.NorthWest))]
	public void EveryDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = EveryDirection.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(OrthogonalDirection.Right),	nameof(RotationType.Invert),			nameof(OrthogonalDirection.Left))]
	[InlineData(nameof(OrthogonalDirection.Down),	nameof(RotationType.Clockwise),			nameof(OrthogonalDirection.Left))]
	[InlineData(nameof(OrthogonalDirection.Up),		nameof(RotationType.Invert),			nameof(OrthogonalDirection.Down))]
	[InlineData(nameof(OrthogonalDirection.Right),	nameof(RotationType.CounterClockwise),	nameof(OrthogonalDirection.Up))]
	public void OrthogonalDirection_Turn_IsCorrect(string directionName, string rotationTypeName, string expectedDirectionName)
	{
		var direction = OrthogonalDirection.GetSingleMember(directionName);
		var rotationType = RotationType.GetSingleMember(rotationTypeName);
		var newDirection = direction.GetDirectionFromTurn(rotationType);

		Assert.Equal(expectedDirectionName, newDirection.Name);
	}

	[Theory]
	[InlineData(nameof(EveryDirection.North),	nameof(OrthogonalDirection.Up))]
	[InlineData(nameof(EveryDirection.East),	nameof(OrthogonalDirection.Right))]
	[InlineData(nameof(EveryDirection.South),	nameof(OrthogonalDirection.Down))]
	[InlineData(nameof(EveryDirection.West),	nameof(OrthogonalDirection.Left))]
	public void Direction_Convert_ShouldWork(string directionName, string expectedDirectionName)
	{
		var deltaPoint = EveryDirection.GetSingleMember(directionName);
		var newDirectionName = EveryDirection.Convert<OrthogonalDirection, int>(deltaPoint).Name;

		Assert.Equal(newDirectionName, expectedDirectionName);
	}
}