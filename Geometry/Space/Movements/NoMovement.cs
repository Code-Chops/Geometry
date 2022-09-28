using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public record NoMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override IDirection GetDirection() => this._direction;
	private readonly IDirection _direction;
	
	protected sealed override Point<TNumber> CalculatePoint(Point<TNumber> _, IStopwatch __) => this.StartingPoint;

	public NoMovement(Point<TNumber> point, IDirection? direction = null)
		: base(startingPoint: point)
	{
		this._direction = direction ?? NoDirection<TNumber>.Instance;
	}
}