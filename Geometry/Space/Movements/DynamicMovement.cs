using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement in which the direction and location can be determined by using a formula with the elapsed milliseconds as a parameter.
/// </summary>
public abstract record DynamicMovement<TNumber> : Movement<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"{this.GetType().Name}: {this.Point}, start: {this.StartingPoint}, direction: {this.Direction}, elapsed: {this.Stopwatch.ElapsedMilliseconds}";

	public override IDirection Direction => new FreeDirection<TNumber>(this.Point);

	protected DynamicMovement(Point<TNumber> startingPoint)
		: base(startingPoint)
	{
	}
}