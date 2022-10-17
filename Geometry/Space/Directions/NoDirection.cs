using CodeChops.Geometry.Space.Points;

namespace CodeChops.Geometry.Space.Directions;

public sealed record NoDirection<TNumber> : IDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public Point<TNumber> Value => Point<TNumber>.Default;

	public Point<TTargetNumber> GetValue<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible 
		=> Point<TTargetNumber>.Default;

	public static readonly NoDirection<TNumber> Instance = new();
	
	// Should be private because it's a singleton.
	private NoDirection()
	{
	}
}