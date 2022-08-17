namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement which direction changes over time. It is still deterministic as a formula is being used to calculate the movement using elapsed milliseconds.
/// </summary>
public abstract record DynamicMovement<TNumber> : DeterministicMovement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	protected DynamicMovement(Point<TNumber> startPoint)
		: base(startPoint)
	{
	}
}