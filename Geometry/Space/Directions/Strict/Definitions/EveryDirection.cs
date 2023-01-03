namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

[DisableConcurrency]
public sealed record EveryDirection<TNumber> : StrictDirection<EveryDirection<TNumber>, TNumber>
	where TNumber : INumber<TNumber>
{
	public static readonly EveryDirection<TNumber> North 		= CreatePoint( 0, -1);
	public static readonly EveryDirection<TNumber> NorthEast 	= CreatePoint( 1, -1);
	public static readonly EveryDirection<TNumber> East 		= CreatePoint( 1,  0);
	public static readonly EveryDirection<TNumber> SouthEast 	= CreatePoint( 1,  1);
	public static readonly EveryDirection<TNumber> South 		= CreatePoint( 0,  1);
	public static readonly EveryDirection<TNumber> SouthWest 	= CreatePoint(-1,  1);
	public static readonly EveryDirection<TNumber> West 		= CreatePoint(-1,  0);
	public static readonly EveryDirection<TNumber> NorthWest 	= CreatePoint(-1, -1);
}