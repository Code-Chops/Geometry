using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement that can be determined by using the elapsed milliseconds.
/// </summary>
public abstract record DeterministicMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"{this.GetType().Name}: {this.Point}, start: {this.StartPoint}, direction: {this.CalculatePoint(this.Stopwatch.ElapsedMilliseconds)}, elapsed: {this.Stopwatch.ElapsedMilliseconds}";

	public override Point<TNumber> Point => this.StartPoint + this.CalculatePoint(this.Stopwatch.ElapsedMilliseconds);
	public Point<TNumber> StartPoint { get; }
	public IStopwatch Stopwatch { get; }
	
	protected abstract Point<TNumber> CalculatePoint(float elapsedMilliseconds);

	protected DeterministicMovement(Point<TNumber> startPoint)
	{
		this.StartPoint = startPoint;
		this.Stopwatch = StopwatchScope.Current.Value;
		this.Stopwatch.Start();
	}
}