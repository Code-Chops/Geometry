namespace CodeChops.Geometry.Space.Directions;

public interface IDirection<TNumber> : IDirection
	where TNumber : INumber<TNumber>
{
	Point<TNumber> Value { get; }
}

/// <summary>
/// A direction holds a delta point.
/// </summary>
public interface IDirection : IValueObject
{
}