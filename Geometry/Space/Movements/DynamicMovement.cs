using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement in which the direction and location can be determined by using a formula with the elapsed milliseconds as a parameter.
/// </summary>
public abstract record DynamicMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override IDirection GetDirection() => new FreeDirection<TNumber>(this.Point);

	protected DynamicMovement(Point<TNumber> startingPoint)
		: base(startingPoint)
	{
	}
}