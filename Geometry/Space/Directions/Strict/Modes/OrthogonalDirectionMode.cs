namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

public partial record OrthogonalDirectionMode<TNumber> : StrictDirection<OrthogonalDirectionMode<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static OrthogonalDirectionMode<TNumber> Up { get; }		= CreateMember( 0, -1);
	public static OrthogonalDirectionMode<TNumber> Right { get; }	= CreateMember( 1,  0);
	public static OrthogonalDirectionMode<TNumber> Down { get; }	= CreateMember( 0,  1);
	public static OrthogonalDirectionMode<TNumber> Left { get; }	= CreateMember(-1,  0);
}