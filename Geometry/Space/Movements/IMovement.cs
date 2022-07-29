using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public interface IMovement : IValueObject
{
	Point<float> GetPoint();
	Point<float> GetDirectionDeltaPoint();

	IDirection GetDirection();
}