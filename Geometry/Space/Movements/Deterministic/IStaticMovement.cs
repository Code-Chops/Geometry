namespace CodeChops.Geometry.Space.Movements.Deterministic;

/// <summary>
/// A movement that only goes into one strict direction over its lifetime.
/// </summary>
public interface IStaticMovement
{
	string DirectionModeName { get; }
}
