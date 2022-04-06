using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement<TDeltaPointNumber> : IMovement
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	public Point<float> GetPoint() => this.Point.Cast<TDeltaPointNumber, float>();
	protected abstract Point<TDeltaPointNumber> Point { get; }
	public abstract Point<float> GetDirectionDeltaPoint();
	public abstract IDirection GetDirection();
}