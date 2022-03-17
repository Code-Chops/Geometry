namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record DiagonalDirection : StrictDirection<DiagonalDirection, int>
{
	public static DiagonalDirection NorthEast { get; } = CreateMember(( 1, -1));
	public static DiagonalDirection SouthEast { get; } = CreateMember(( 1,  1));
	public static DiagonalDirection SouthWest { get; } = CreateMember((-1,  1));
	public static DiagonalDirection NorthWest { get; } = CreateMember((-1, -1));
}