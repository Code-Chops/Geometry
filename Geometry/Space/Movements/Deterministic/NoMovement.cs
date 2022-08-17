using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public record NoMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override string ToString() => this.GetType().Name;

	public override IDirection Direction { get; }
	public sealed override Point<TNumber> Point { get; }

	public NoMovement(Point<TNumber> point, IDirection? direction = null)
	{
		this.Point = point;
		this.Direction = direction ?? NoDirection<TNumber>.Instance;
	}
}