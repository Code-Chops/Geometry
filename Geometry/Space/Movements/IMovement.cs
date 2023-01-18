using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves from a starting point, using a timer.
/// </summary>
public interface IMovement<out TDirection, TNumber> : IMovement
	where TDirection : IDirection<TNumber>
	where TNumber : INumber<TNumber>
{
	Point<TNumber> StartingPoint { get; }
	TDirection Direction { get; }
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public interface IMovement : IValueObject
{
	IDirection GetDirection();
}