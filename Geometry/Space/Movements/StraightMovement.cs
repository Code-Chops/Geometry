using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Space.Movements.Steps;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public record StraightMovement<TStrictDirection, TNumber> : Movement<TNumber>
	where TStrictDirection : StrictDirection<TStrictDirection, TNumber>, new()
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override TStrictDirection GetDirection() => this._direction;
	private readonly TStrictDirection _direction;
	
	protected sealed override Point<TNumber> CalculatePoint() => this.StartingPoint + this.GetDirection().Value * (Number<TNumber>)Convert.ChangeType(this.StepCounter.Steps, typeof(TNumber));
	
	public StraightMovement(Point<TNumber> startingPoint, TStrictDirection direction, IStepCounter? stepCounter = null)
		: base(startingPoint, stepCounter)
	{
		this._direction = direction;
	}
}