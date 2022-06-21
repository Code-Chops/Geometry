namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record DiagonalDirection<TNumber> : StrictDirection<DiagonalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static DiagonalDirection<TNumber> NorthEast { get; } = CreateMember( 1, -1);
	public static DiagonalDirection<TNumber> SouthEast { get; } = CreateMember( 1,  1);
	public static DiagonalDirection<TNumber> SouthWest { get; } = CreateMember(-1,  1);
	public static DiagonalDirection<TNumber> NorthWest { get; } = CreateMember(-1, -1);
}