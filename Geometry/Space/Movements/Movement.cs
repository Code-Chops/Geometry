using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Movements.Steps;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves from a starting point, using a step counter.
/// </summary>
public abstract record Movement<TNumber> : Movement
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(Movement<TNumber> value) => value.Point;

	public Point<TNumber> Point => this.CalculatePoint();
	public abstract override IDirection<TNumber> GetDirection();
	public Point<TNumber> StartingPoint { get; }
	public IStepCounter StepCounter { get; }
	
	protected abstract Point<TNumber> CalculatePoint();
	
	protected Movement(Point<TNumber> startingPoint, IStepCounter? stepCounter = null)
	{
		this.StartingPoint = startingPoint;
		this.StepCounter = stepCounter ?? StepCounterScope.Current.Value;
		
		this.StepCounter.Start();
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