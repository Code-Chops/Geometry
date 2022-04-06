namespace CodeChops.Geometry.Space.Directions;

public interface IDirection<TDeltaPointNumber> : IDirection
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	Point<TDeltaPointNumber> Value { get; }
}

/// <summary>
/// A direction which holds a delta point.
/// </summary>
public interface IDirection
{
	Point<TTargetPointNumber> GetValue<TTargetPointNumber>()
		where TTargetPointNumber : struct, IComparable<TTargetPointNumber>, IEquatable<TTargetPointNumber>, IConvertible;
}