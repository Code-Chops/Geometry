namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record OrthogonalDirection<TNumber> : StrictDirection<OrthogonalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static OrthogonalDirection<TNumber> Up { get; }		= CreatePoint( 0, -1);
	public static OrthogonalDirection<TNumber> Right { get; }	= CreatePoint( 1,  0);
	public static OrthogonalDirection<TNumber> Down { get; }	= CreatePoint( 0,  1);
	public static OrthogonalDirection<TNumber> Left { get; }	= CreatePoint(-1,  0);
}