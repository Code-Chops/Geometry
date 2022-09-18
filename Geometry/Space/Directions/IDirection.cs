using CodeChops.Geometry.Space.Points;

namespace CodeChops.Geometry.Space.Directions;

public interface IDirection<TNumber> : IDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	Point<TNumber> Value { get; }
}

/// <summary>
/// A direction which holds a delta point.
/// </summary>
public interface IDirection : IValueObject
{
	Point<TTargetNumber> GetValue<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;
}