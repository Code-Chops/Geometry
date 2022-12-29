using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Movements.Steps;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Describes in what direction an object moves using steps.
/// </summary>
public abstract record Movement<TNumber> : Movement
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public sealed override Point<TTargetNumber> GetPoint<TTargetNumber>()
		=> this.Point.Convert<TTargetNumber>();

	public Point<TNumber> Point => this.CalculatePoint();
	public abstract override IDirection GetDirection();
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
	
	public abstract Point<TTargetNumber> GetPoint<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;

	public abstract IDirection GetDirection();
}