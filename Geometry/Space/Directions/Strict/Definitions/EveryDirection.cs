namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record EveryDirection<TNumber> : StrictDirection<EveryDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static EveryDirection<TNumber> North { get; }		= CreateMember( 0, -1);
	public static EveryDirection<TNumber> NorthEast { get; }	= CreateMember( 1, -1);
	public static EveryDirection<TNumber> East { get; }			= CreateMember( 1,  0);
	public static EveryDirection<TNumber> SouthEast { get; }	= CreateMember( 1,  1);
	public static EveryDirection<TNumber> South { get; }		= CreateMember( 0,  1);
	public static EveryDirection<TNumber> SouthWest { get; }	= CreateMember(-1,  1);
	public static EveryDirection<TNumber> West { get; }			= CreateMember(-1,  0);
	public static EveryDirection<TNumber> NorthWest { get; }	= CreateMember(-1, -1);
}