using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves from a starting point, using a timer.
/// </summary>
public interface IMovement<TNumber> : IMovement
	where TNumber : INumber<TNumber>
{
	IDirection<TNumber> Direction { get; }

	Point<TNumber> StartingPoint { get; }
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public interface IMovement : IValueObject
{
	Point<TTargetNumber> GetDeltaPoint<TTargetNumber>() 
		where TTargetNumber : INumber<TTargetNumber>;
}