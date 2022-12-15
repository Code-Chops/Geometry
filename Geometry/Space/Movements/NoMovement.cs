using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public record NoMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override IDirection GetDirection() => this._direction;
	private readonly IDirection _direction;
	
	protected sealed override Point<TNumber> CalculatePoint() => this.StartingPoint;

	public NoMovement(Point<TNumber> point, IDirection? direction = null)
		: base(startingPoint: point)
	{
		this._direction = direction ?? NoDirection<TNumber>.Instance;
	}
}