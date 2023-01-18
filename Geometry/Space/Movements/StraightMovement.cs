using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public readonly record struct StraightMovement<TStrictDirection, TNumber> : IMovement<TStrictDirection, TNumber>
	where TStrictDirection : StrictDirection<TStrictDirection, TNumber>, new()
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(StraightMovement<TStrictDirection, TNumber> value) => value.Point;

	public Point<TNumber> Point => this.StartingPoint + this.Direction.Value * this.DistanceFactorGetter(this.Timer);
	public Point<TNumber> StartingPoint { get; }

	public TStrictDirection Direction { get; }
	private Func<ITimer, TNumber> DistanceFactorGetter { get; }
	
	private ITimer Timer { get; }
	
	/// <param name="distanceFactorGetter">This factor will be multiplied by the direction and added to the starting point.</param>
	public StraightMovement(Point<TNumber> startingPoint, TStrictDirection direction, ITimer timer, Func<ITimer, TNumber> distanceFactorGetter)
	{
		this.StartingPoint = startingPoint;
		this.Direction = direction ?? throw new ArgumentNullException(nameof(direction));
		this.Timer = timer ?? throw new ArgumentNullException(nameof(timer));
		this.DistanceFactorGetter = distanceFactorGetter ?? throw new ArgumentNullException(nameof(distanceFactorGetter));
	}
	
	public StraightMovement(Point<TNumber> startingPoint, TStrictDirection direction)
	{
		this.StartingPoint = startingPoint;
		this.Direction = direction ?? throw new ArgumentNullException(nameof(direction));
		this.Timer = null!;
		this.DistanceFactorGetter = _ => TNumber.One;
	}
	
	public Point<TTargetNumber> GetDeltaPoint<TTargetNumber>() 
		where TTargetNumber : INumber<TTargetNumber> 
		=> this.Direction.Value.Convert<TTargetNumber>();
}