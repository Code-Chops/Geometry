namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record EveryDirection<TNumber> : StrictDirection<EveryDirection<TNumber>, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static EveryDirection<TNumber> North { get; }		= CreatePoint( 0, -1);
	public static EveryDirection<TNumber> NorthEast { get; }	= CreatePoint( 1, -1);
	public static EveryDirection<TNumber> East { get; }			= CreatePoint( 1,  0);
	public static EveryDirection<TNumber> SouthEast { get; }	= CreatePoint( 1,  1);
	public static EveryDirection<TNumber> South { get; }		= CreatePoint( 0,  1);
	public static EveryDirection<TNumber> SouthWest { get; }	= CreatePoint(-1,  1);
	public static EveryDirection<TNumber> West { get; }			= CreatePoint(-1,  0);
	public static EveryDirection<TNumber> NorthWest { get; }	= CreatePoint(-1, -1);
}