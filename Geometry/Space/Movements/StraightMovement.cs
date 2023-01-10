using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Time.Moments;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement that only goes into one straight direction over its lifetime.
/// </summary>
public record StraightMovement<TStrictDirection, TNumber> : Movement<TNumber>
	where TStrictDirection : StrictDirection<TStrictDirection, TNumber>, new()
	where TNumber : INumber<TNumber>
{
	public sealed override TStrictDirection GetDirection() => this._direction;
	private readonly TStrictDirection _direction;
	
	public TNumber Multiplier { get; }
	
	protected sealed override Point<TNumber> CalculatePoint() 
		=> this.StartingPoint + this.GetDirection().Value * this.MomentCounter.GetMoments<TNumber>() * this.Multiplier;

	public StraightMovement(Point<TNumber> startingPoint, TStrictDirection direction, TNumber multiplier, IMomentCounter? momentCounter = null)
		: base(startingPoint, momentCounter)
	{
		this._direction = direction;
		this.Multiplier = multiplier;
	}
}