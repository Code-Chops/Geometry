namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

public partial record DiagonalDirectionMode<TNumber> : StrictDirection<DiagonalDirectionMode<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static DiagonalDirectionMode<TNumber> NorthEast { get; } = CreateMember( 1, -1);
	public static DiagonalDirectionMode<TNumber> SouthEast { get; } = CreateMember( 1,  1);
	public static DiagonalDirectionMode<TNumber> SouthWest { get; } = CreateMember(-1,  1);
	public static DiagonalDirectionMode<TNumber> NorthWest { get; } = CreateMember(-1, -1);
}