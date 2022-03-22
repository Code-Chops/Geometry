namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record VerticalDirection<TNumber> : StrictDirection<VerticalDirection<TNumber>>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static VerticalDirection<TNumber> Up { get; }	= CreateMember(0, -1);
	public static VerticalDirection<TNumber> Down { get; }	= CreateMember(0, 1);
}