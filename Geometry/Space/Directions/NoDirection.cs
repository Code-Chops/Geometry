namespace CodeChops.Geometry.Space.Directions;

public record NoDirection : IDirection
{
	public string Name { get; } = nameof(NoDirection);
	public Point<float> GetValue() => Point<float>.Empty;

	public static readonly NoDirection Instance = new();

	private NoDirection()
	{
	}
}
