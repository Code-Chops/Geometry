using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement which direction changes over time. It is still deterministic as a formula is being used to calculate the movement using elapsed milliseconds.
/// </summary>
public abstract record DynamicMovement<TPointNumber> : DeterministicMovement<TPointNumber>
	where TPointNumber : struct, IComparable<TPointNumber>, IEquatable<TPointNumber>, IConvertible
{
	public sealed override string ToString() => $"{this.GetType().Name} ({nameof(DeterministicMovement<TPointNumber>)}): {this.GetDirectionDeltaPoint()}";

	public sealed override IDirection GetDirection() => new FreeDirection<float>(this.GetDirectionDeltaPoint());

	public DynamicMovement(Point<TPointNumber> startPoint, float speed)
		: base(startPoint, speed)
	{
	}
}