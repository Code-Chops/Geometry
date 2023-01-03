namespace CodeChops.Geometry.Space.Directions.Strict;

[DisableConcurrency]
public sealed record RotationType : MagicEnum<RotationType>
{
	public static readonly RotationType CounterClockwise	= CreateMember(-1);
	public static readonly RotationType Invert				= CreateMember( 0);
	public static readonly RotationType Clockwise			= CreateMember( 1);
}