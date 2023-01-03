namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record VerticalDirection<TNumber> : StrictDirection<VerticalDirection<TNumber>, TNumber>
	where TNumber : INumber<TNumber>
{
	public static readonly VerticalDirection<TNumber> Up	= CreatePoint(0, -1);
	public static readonly VerticalDirection<TNumber> Down 	= CreatePoint(0,  1);
}