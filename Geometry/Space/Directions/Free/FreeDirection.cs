namespace CodeChops.Geometry.Space.Directions.Free;

/// <summary>
/// A direction with every possible delta point.
/// </summary>
public record FreeDirection<TNumber> : ValueObject, IDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"{nameof(FreeDirection<TNumber>)}: {this.Value}";

	public Point<TNumber> Value { get; }

	public Point<TTargetNumber> GetValue<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return this.Value.Cast<TNumber, TTargetNumber>();
	}

	/// <summary>
	/// The angle (from 0 to 360 degrees).
	/// </summary>
	public double Angle { get; }

	public FreeDirection(Point<TNumber> value)
	{
		this.Value = value;
		this.Angle = this.Value.ToAngle();
	}

	public FreeDirection(double angle)
	{
		this.Value = new Point<TNumber>(angle);
		this.Angle = angle;
	}

	public static FreeDirection<TNumber> GetNewDirectionFromRandomTurn()
	{
		return new(new Random().NextDouble() * 360);
	}

	public FreeDirection<TNumber> GetNewDirectionFromTurn(double angleDelta)
	{
		return new(this.Angle + angleDelta);
	}
}