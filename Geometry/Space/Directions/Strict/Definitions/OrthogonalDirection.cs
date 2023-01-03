namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record OrthogonalDirection<TNumber> : StrictDirection<OrthogonalDirection<TNumber>, TNumber>
	where TNumber : INumber<TNumber>
{
	public static readonly OrthogonalDirection<TNumber> Up 		= CreatePoint( 0, -1);
	public static readonly OrthogonalDirection<TNumber> Right 	= CreatePoint( 1,  0);
	public static readonly OrthogonalDirection<TNumber> Down 	= CreatePoint( 0,  1);
	public static readonly OrthogonalDirection<TNumber> Left 	= CreatePoint(-1,  0);
}