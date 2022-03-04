using CodeChops.GenericMath;
using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements.NonDeterministic;

/// <summary>
/// A movement that is possible in every direction in each movement, and is therefore non-deterministic.
/// </summary>
public record FreeMovement<TPointNumber> : Movement<TPointNumber>
	where TPointNumber : struct, IComparable<TPointNumber>, IEquatable<TPointNumber>, IConvertible
{
	public sealed override string ToString() => this.GetType().Name;

	protected sealed override Point<TPointNumber> Point => this._point;
	private Point<TPointNumber> _point;

	public sealed override IDirection GetDirection() => this.Direction;
	public FreeDirection<TPointNumber> Direction { get; set; }

	public sealed override Point<float> DirectionDeltaPoint => this.Direction.GetValue();

	public void MoveRelative(float step)
	{
		this._point = this.Direction.DeltaPoint * Number<TPointNumber>.Create(step);
	}

	public void MoveAbsolute(Point<TPointNumber> point)
	{
		this._point = point;
	}

	public FreeMovement(FreeDirection<TPointNumber> direction)
	{
		this.Direction = direction;
	}
}
