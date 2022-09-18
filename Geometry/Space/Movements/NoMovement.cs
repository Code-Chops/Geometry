using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public sealed record NoMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => this.GetType().Name;
	
	public override IDirection Direction { get; }
	protected override Point<TNumber> CalculatePoint(Point<TNumber> _, IStopwatch __) => this.StartingPoint;

	public NoMovement(Point<TNumber> point, IDirection? direction = null)
		: base(startingPoint: point)
	{
		this.Direction = direction ?? NoDirection<TNumber>.Instance;
	}
}