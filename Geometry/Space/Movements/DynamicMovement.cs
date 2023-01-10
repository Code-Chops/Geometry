using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement in which the direction and location can be determined by using a formula with a moment counter.
/// </summary>
public abstract record DynamicMovement<TNumber> : Movement<TNumber>
	where TNumber : INumber<TNumber>
{
	public sealed override IDirection<TNumber> GetDirection()
	{
		var point = this.Point;
		
		if (point != this._currentPoint)
		{
			this._currentPoint = point;
			this._currentDirection = new FreeDirection<TNumber>(point);
		}

		return this._currentDirection;
	}

	private FreeDirection<TNumber> _currentDirection;
	private Point<TNumber> _currentPoint;
	
	protected DynamicMovement(Point<TNumber> startingPoint)
		: base(startingPoint)
	{
	}
}