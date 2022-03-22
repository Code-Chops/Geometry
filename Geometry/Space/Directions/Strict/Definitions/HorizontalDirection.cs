namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record HorizontalDirection<TNumber> : StrictDirection<HorizontalDirection<TNumber>>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static HorizontalDirection<TNumber> Right { get; }	= CreateMember( 1, 0);
	public static HorizontalDirection<TNumber> Left { get; }	= CreateMember(-1, 0);
}