namespace CodeChops.Geometry.Space.Directions.Strict.Definitions;

public record EveryDirection : StrictDirection<EveryDirection>
{
	public static EveryDirection North { get; } = CreateMember((0, -1));
	public static EveryDirection NorthEast { get; } = CreateMember((1, -1));
	public static EveryDirection East { get; } = CreateMember((1, 0));
	public static EveryDirection SouthEast { get; } = CreateMember((1, 1));
	public static EveryDirection South { get; } = CreateMember((0, 1));
	public static EveryDirection SouthWest { get; } = CreateMember((-1, 1));
	public static EveryDirection West { get; } = CreateMember((-1, 0));
	public static EveryDirection NorthWest { get; } = CreateMember((-1, -1));
}