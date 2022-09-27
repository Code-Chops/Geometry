namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record HorizontalDirection<TNumber> : StrictDirection<HorizontalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static readonly HorizontalDirection<TNumber> Right	= CreatePoint( 1, 0);
	public static readonly HorizontalDirection<TNumber> Left 	= CreatePoint(-1, 0);
}