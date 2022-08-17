using CodeChops.Geometry.Space.Directions.Strict;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public record StraightMovement<TStrictDirection, TNumber> : DynamicMovement<TNumber>
	where TStrictDirection : StrictDirection<TStrictDirection, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override string ToString() => $"{this.GetType().Name}: {this.Direction}";

	public override TStrictDirection Direction { get; }
	protected sealed override Point<TNumber> CalculatePoint(float elapsedMilliseconds) => this.Direction.Value * Number<TNumber>.Create(elapsedMilliseconds);

	public StraightMovement(Point<TNumber> startPoint, TStrictDirection direction)
		: base(startPoint)
	{
		this.Direction = direction;
	}
}