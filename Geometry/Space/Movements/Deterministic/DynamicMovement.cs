using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement which direction changes over time. It is still deterministic as a formula is being used to calculate the movement using elapsed milliseconds.
/// </summary>
public record DynamicMovement<TPointNumber> : DeterministicMovement<TPointNumber>
	where TPointNumber : struct, IComparable<TPointNumber>, IEquatable<TPointNumber>, IConvertible
{
	public sealed override string ToString() => $"{this.GetType().Name} ({nameof(DeterministicMovement<TPointNumber>)}): {this.DirectionDeltaPoint}";

	public sealed override IDirection GetDirection() => new FreeDirection<float>(this.DirectionDeltaPoint);
	protected sealed override Point<TPointNumber> CalculatePoint(float step) => this._calculatePoint(step);
	private readonly Func<float, Point<TPointNumber>> _calculatePoint;

	public DynamicMovement(Point<TPointNumber> startPoint, float speed, Func<float, Point<TPointNumber>> pointCalculator)
		: base(startPoint, speed)
	{
		this._calculatePoint = pointCalculator;
	}
}