using System.Runtime.InteropServices;
using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly record struct NoMovement<TNumber> : IMovement<TNumber>, IHasDefault<NoMovement<TNumber>>
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(NoMovement<TNumber> value) => value.Point;
	
	public static NoMovement<TNumber> Default { get; } = new(Point<TNumber>.Default);

	public Point<TNumber> Point { get; }
	public Point<TNumber> StartingPoint => this.Point;
	
	public IDirection<TNumber> Direction { get; }
	public IDirection GetDirection() => this.Direction;

	private NoMovement(Point<TNumber> point, IDirection<TNumber>? direction = null)
	{
		this.Point = point;
		this.Direction = direction ?? NoDirection<TNumber>.Instance;
	}

	public Point<TTargetNumber> GetDeltaPoint<TTargetNumber>() 
		where TTargetNumber : INumber<TTargetNumber>
	{
		return this.Direction.Value.Convert<TTargetNumber>();
	}
}