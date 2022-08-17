namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

[DisableConcurrency]
public partial record HorizontalDirection<TNumber> : StrictDirection<HorizontalDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static HorizontalDirection<TNumber> Right { get; }	= CreatePoint( 1, 0);
	public static HorizontalDirection<TNumber> Left { get; }	= CreatePoint(-1, 0);
}