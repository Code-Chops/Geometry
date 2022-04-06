namespace CodeChops.Geometry.Space.Directions;

public record NoDirection : IDirection
{
	public string Name { get; } = nameof(NoDirection);

	public Point<TTargetPointNumber> GetValue<TTargetPointNumber>()
		where TTargetPointNumber : struct, IComparable<TTargetPointNumber>, IEquatable<TTargetPointNumber>, IConvertible 
			=> Point<TTargetPointNumber>.Empty;

	public static readonly NoDirection Instance = new();

	private NoDirection()
	{
	}
}