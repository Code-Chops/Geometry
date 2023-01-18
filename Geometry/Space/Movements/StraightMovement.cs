using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public readonly record struct StraightMovement<TDirection, TNumber> : IMovement<TDirection, TNumber>
	where TDirection : IDirection<TNumber>
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(StraightMovement<TDirection, TNumber> value) => value.Point;

	public Point<TNumber> Point => this.StartingPoint + this.Direction.Value * this.DistanceFactorGetter(this.Timer);
	public Point<TNumber> StartingPoint { get; }

	public TDirection Direction { get; }
	public IDirection GetDirection() => this.Direction;
	private Func<ITimer, TNumber> DistanceFactorGetter { get; }
	
	private ITimer Timer { get; }
	
	/// <param name="distanceFactorGetter">This factor will be multiplied by the direction and added to the starting point.</param>
	public StraightMovement(Point<TNumber> startingPoint, TDirection direction, ITimer timer, Func<ITimer, TNumber> distanceFactorGetter)
	{
		this.StartingPoint = startingPoint;
		this.Direction = direction ?? throw new ArgumentNullException(nameof(direction));
		this.Timer = timer ?? throw new ArgumentNullException(nameof(timer));
		this.DistanceFactorGetter = distanceFactorGetter ?? throw new ArgumentNullException(nameof(distanceFactorGetter));
	}
	
	public StraightMovement(Point<TNumber> startingPoint, TDirection direction)
	{
		this.StartingPoint = startingPoint;
		this.Direction = direction ?? throw new ArgumentNullException(nameof(direction));
		this.Timer = null!;
		this.DistanceFactorGetter = _ => TNumber.One;
	}
}