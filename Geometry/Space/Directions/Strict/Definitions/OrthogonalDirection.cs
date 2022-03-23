namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record OrthogonalDirection<TNumber> : StrictDirection<OrthogonalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static OrthogonalDirection<TNumber> Up { get; }		= CreateMember( 0, -1);
	public static OrthogonalDirection<TNumber> Right { get; }	= CreateMember( 1,  0);
	public static OrthogonalDirection<TNumber> Down { get; }	= CreateMember( 0,  1);
	public static OrthogonalDirection<TNumber> Left { get; }	= CreateMember(-1,  0);
}