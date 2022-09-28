using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement<TNumber> : Movement
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override Point<TTargetNumber> GetPoint<TTargetNumber>()
		=> this.Point.Convert<TTargetNumber>();

	public Point<TNumber> Point => this.CalculatePoint(this.StartingPoint, this.Stopwatch);
	public abstract override IDirection GetDirection();
	public Point<TNumber> StartingPoint { get; }
	public IStopwatch Stopwatch { get; }
	
	protected abstract Point<TNumber> CalculatePoint(Point<TNumber> startingPoint, IStopwatch stopWatch);
	
	protected Movement(Point<TNumber> startingPoint)
	{
		this.StartingPoint = startingPoint;
		this.Stopwatch = StopwatchScope.Current.Value;
		this.Stopwatch.Start();
	}
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement : IValueObject
{
	public override string ToString() => this.ToDisplayString();
	
	public abstract Point<TTargetNumber> GetPoint<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;

	public abstract IDirection GetDirection();
}