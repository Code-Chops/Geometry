using CodeChops.GenericMath;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement that can be determined by using the elapsed milliseconds.
/// </summary>
public abstract record DeterministicMovement<TPointNumber> : Movement<TPointNumber>
	where TPointNumber : struct, IComparable<TPointNumber>, IEquatable<TPointNumber>, IConvertible
{
	protected override Point<TPointNumber> Point => this.StartPoint + this.CalculatePoint(this.Stopwatch.ElapsedMilliseconds) * Number<TPointNumber>.Create(this.Speed);
	protected abstract Point<TPointNumber> CalculatePoint(float step);
	public Point<TPointNumber> StartPoint { get; }
	public float Speed { get; }
	public IStopwatch Stopwatch { get; }

	/// <summary>
	/// Not sure if this is correct.
	/// </summary>
	public override Point<float> GetDirectionDeltaPoint() => this.CalculatePoint(this.Stopwatch.ElapsedMilliseconds).Cast<TPointNumber, float>();

	protected DeterministicMovement(Point<TPointNumber> startPoint, float speed)
	{
		this.StartPoint = startPoint;
		this.Speed = speed;
		this.Stopwatch = StopwatchScope.Current.Value;
		this.Stopwatch.Start();
	}
}