using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public record NoMovement<TPointNumber> : Movement<TPointNumber>
	where TPointNumber : struct, IComparable<TPointNumber>, IEquatable<TPointNumber>, IConvertible
{
	public sealed override string ToString() => this.GetType().Name;

	public sealed override Point<float> DirectionDeltaPoint => Point<float>.Empty;
	public sealed override IDirection GetDirection() => this.Direction;

	public IDirection Direction { get; }
	protected sealed override Point<TPointNumber> Point { get; }

	public NoMovement(Point<TPointNumber> point, IDirection? direction = null)
	{
		this.Point = point;
		this.Direction = direction ?? NoDirection.Instance;
	}
}
