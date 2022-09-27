namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record DiagonalDirection<TNumber> : StrictDirection<DiagonalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static readonly DiagonalDirection<TNumber> NorthEast = CreatePoint( 1, -1);
	public static readonly DiagonalDirection<TNumber> SouthEast = CreatePoint( 1,  1);
	public static readonly DiagonalDirection<TNumber> SouthWest = CreatePoint(-1,  1);
	public static readonly DiagonalDirection<TNumber> NorthWest = CreatePoint(-1, -1);
}