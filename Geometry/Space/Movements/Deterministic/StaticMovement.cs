using CodeChops.GenericMath;
using CodeChops.Geometry.Space.Directions.Strict;

namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement that only goes into one strict direction over its lifetime.
/// </summary>
public record StaticMovement<TStrictDirection, TDeltaPointNumber> : DeterministicMovement<TDeltaPointNumber>, IStaticMovement
	where TStrictDirection : StrictDirection<TStrictDirection, TDeltaPointNumber>
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	public sealed override string ToString() => $"{this.GetType().Name}: {this.Direction}";

	public sealed override IStrictDirection<TDeltaPointNumber> GetDirection() => this.Direction;
	public string DirectionTypeName { get; } = typeof(TStrictDirection).Name;
	public TStrictDirection Direction { get; init; }
	protected sealed override Point<TDeltaPointNumber> CalculatePoint(float step) => this.Direction.Value * Number<TDeltaPointNumber>.Create(step);

	public StaticMovement(Point<TDeltaPointNumber> startPoint, float speed, TStrictDirection direction)
		: base(startPoint, speed)
	{
		this.Direction = direction;
	}
}