using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public sealed record StraightMovement<TStrictDirection, TNumber> : Movement<TNumber>
	where TStrictDirection : StrictDirection<TStrictDirection, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"{this.GetType().Name}: {this.Direction}";

	public override TStrictDirection Direction { get; }
	protected override Point<TNumber> CalculatePoint(Point<TNumber> startingPoint, IStopwatch stopWatch) => this.StartingPoint + this.Direction.Value * (Number<TNumber>)Convert.ChangeType(stopWatch.ElapsedMilliseconds, typeof(TNumber));
	public float Speed { get; }
	
	public StraightMovement(Point<TNumber> startingPoint, TStrictDirection direction, float speed)
		: base(startingPoint)
	{
		this.Direction = direction;
		this.Speed = speed;
	}
}