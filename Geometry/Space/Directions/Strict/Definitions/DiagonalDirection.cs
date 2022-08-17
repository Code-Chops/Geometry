namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public partial record DiagonalDirection<TNumber> : StrictDirection<DiagonalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static DiagonalDirection<TNumber> NorthEast { get; } = CreatePoint( 1, -1);
	public static DiagonalDirection<TNumber> SouthEast { get; } = CreatePoint( 1,  1);
	public static DiagonalDirection<TNumber> SouthWest { get; } = CreatePoint(-1,  1);
	public static DiagonalDirection<TNumber> NorthWest { get; } = CreatePoint(-1, -1);
}