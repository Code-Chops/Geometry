using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves from a starting point, using a timer.
/// </summary>
public interface IMovement<out TDirection, TNumber> : IMovement<TNumber>
	where TDirection : IDirection<TNumber>
	where TNumber : INumber<TNumber>
{
	TDirection Direction { get; }
}

public interface IMovement<TNumber> : IMovement
	where TNumber : INumber<TNumber>
{
	Point<TNumber> StartingPoint { get; }
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public interface IMovement : IValueObject
{
	IDirection GetDirection();
}