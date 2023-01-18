using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Directions.Free;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// A movement in which the direction and location can be determined by using a formula with a timer.
/// </summary>
public abstract record DynamicMovement<TNumber> : IMovement<IDirection<TNumber>, TNumber> 
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(DynamicMovement<TNumber> value) => value.Point;

	public Point<TNumber> Point => this.CalculatePoint(this.Timer);
	public Point<TNumber> StartingPoint { get; }

	protected abstract Point<TNumber> CalculatePoint(ITimer timer);
	
	private ITimer Timer { get; }

	public IDirection<TNumber> Direction
	{
		get
		{
			if (this.Point != this._currentPoint)
			{
				this._currentPoint = this.Point;
				this._currentDirection = new FreeDirection<TNumber>(this.Point);
			}
	
			return this._currentDirection;
		}
	}

	protected DynamicMovement(Point<TNumber> startingPoint, ITimer timer)
	{
		this.StartingPoint = startingPoint;
		this.Timer = timer;
	}

	private FreeDirection<TNumber> _currentDirection;
	private Point<TNumber> _currentPoint;
	
	public Point<TTargetNumber> GetDeltaPoint<TTargetNumber>() 
		where TTargetNumber : INumber<TTargetNumber>
		=> this.Direction.Value.Convert<TTargetNumber>();
}