namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record OrthogonalDirection : StrictDirection<OrthogonalDirection>
{
	public static OrthogonalDirection Up { get; }		= CreateMember(( 0, -1));
	public static OrthogonalDirection Right { get; }	= CreateMember(( 1,  0));
	public static OrthogonalDirection Down { get; }		= CreateMember(( 0,  1));
	public static OrthogonalDirection Left { get; }		= CreateMember((-1,  0));
}