namespace CodeChops.Geometry.Space.Directions.Strict.Modes;

public partial record EveryDirectionMode<TNumber> : StrictDirection<EveryDirectionMode<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static EveryDirectionMode<TNumber> North { get; }		= CreateMember( 0, -1);
	public static EveryDirectionMode<TNumber> NorthEast { get; }	= CreateMember( 1, -1);
	public static EveryDirectionMode<TNumber> East { get; }			= CreateMember( 1,  0);
	public static EveryDirectionMode<TNumber> SouthEast { get; }	= CreateMember( 1,  1);
	public static EveryDirectionMode<TNumber> South { get; }		= CreateMember( 0,  1);
	public static EveryDirectionMode<TNumber> SouthWest { get; }	= CreateMember(-1,  1);
	public static EveryDirectionMode<TNumber> West { get; }			= CreateMember(-1,  0);
	public static EveryDirectionMode<TNumber> NorthWest { get; }	= CreateMember(-1, -1);
}