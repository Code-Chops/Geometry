namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

public partial record VerticalDirectionMode<TNumber> : StrictDirection<VerticalDirectionMode<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static VerticalDirectionMode<TNumber> Up { get; }	= CreateMember(0, -1);
	public static VerticalDirectionMode<TNumber> Down { get; }	= CreateMember(0,  1);
}