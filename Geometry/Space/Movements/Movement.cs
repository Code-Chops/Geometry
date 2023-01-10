using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Time.Moments;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves from a starting point, using a moment counter.
/// </summary>
public abstract record Movement<TNumber> : Movement
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(Movement<TNumber> value) => value.Point;

	public Point<TNumber> Point => this.CalculatePoint();
	public abstract override IDirection<TNumber> GetDirection();
	public Point<TNumber> StartingPoint { get; }
	public IMomentCounter MomentCounter { get; }
	
	protected abstract Point<TNumber> CalculatePoint();
	
	protected Movement(Point<TNumber> startingPoint, IMomentCounter? momentCounter = null)
	{
		this.StartingPoint = startingPoint;
		this.MomentCounter = momentCounter ?? MomentCounterScope.Current.Value;
		
		this.MomentCounter.Start();
	}
}

/// <summary>
/// Describes in what direction an object moves over time.
/// </summary>
public abstract record Movement : IValueObject
{
	public override string ToString() => this.ToDisplayString();

	public abstract IDirection GetDirection();
}