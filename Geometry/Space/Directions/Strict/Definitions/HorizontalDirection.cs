namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record HorizontalDirection : StrictDirection<HorizontalDirection>
{
	public static HorizontalDirection Right { get; } = CreateMember((1, 0));
	public static HorizontalDirection Left { get; } = CreateMember((-1, 0));
}