namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record VerticalDirection<TNumber> : StrictDirection<VerticalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static readonly VerticalDirection<TNumber> Up	= CreatePoint(0, -1);
	public static readonly VerticalDirection<TNumber> Down 	= CreatePoint(0,  1);
}