namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record VerticalDirection<TNumber> : StrictDirection<VerticalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static VerticalDirection<TNumber> Up { get; }	= CreatePoint(0, -1);
	public static VerticalDirection<TNumber> Down { get; }	= CreatePoint(0,  1);
}