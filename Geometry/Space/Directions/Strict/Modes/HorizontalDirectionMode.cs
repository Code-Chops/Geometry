namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

public partial record HorizontalDirectionMode<TNumber> : StrictDirection<HorizontalDirectionMode<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static HorizontalDirectionMode<TNumber> Right { get; }	= CreateMember( 1, 0);
	public static HorizontalDirectionMode<TNumber> Left { get; }	= CreateMember(-1, 0);
}