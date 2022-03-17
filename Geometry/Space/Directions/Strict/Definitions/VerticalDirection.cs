namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record VerticalDirection : StrictDirection<VerticalDirection>
{
	public static VerticalDirection Up { get; }		= CreateMember((0, -1));
	public static VerticalDirection Down { get; }	= CreateMember((0,  1));
}