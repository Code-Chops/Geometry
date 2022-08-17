using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement<TNumber> : Movement
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override Point<TTargetNumber> GetPoint<TTargetNumber>()
		=> this.Point.Cast<TTargetNumber>();

	public abstract Point<TNumber> Point { get; }
	public abstract override IDirection Direction { get; }
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement : IValueObject
{
	public abstract Point<TTargetNumber> GetPoint<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;
	public abstract IDirection Direction { get; }
}