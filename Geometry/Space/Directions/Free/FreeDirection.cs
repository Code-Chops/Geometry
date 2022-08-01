namespace CodeChops.Geometry.Space.Directions.Free;

/// <summary>
/// A direction with every possible delta point.
/// </summary>
public record FreeDirection<TDeltaPointNumber> : ValueObject, IDirection<TDeltaPointNumber>
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	public override string ToString() => $"{nameof(FreeDirection<TDeltaPointNumber>)}: {this.Value}";

	/// <summary>
	/// Backing field so the value is mutable in this class.
	/// </summary>
	public Point<TDeltaPointNumber> Value { get; private set; }

	public Point<TTargetPointNumber> GetValue<TTargetPointNumber>()
		where TTargetPointNumber : struct, IComparable<TTargetPointNumber>, IEquatable<TTargetPointNumber>, IConvertible
	{
		return this.Value.Cast<TDeltaPointNumber, TTargetPointNumber>();
	}

	/// <summary>
	/// The angle (from 0 to 360 degrees).
	/// </summary>
	public double Angle { get; private set; }

	public FreeDirection(Point<TDeltaPointNumber> deltaPoint)
	{
		this.SetDeltaPoint(deltaPoint);
	}

	public FreeDirection(double angle)
	{
		this.SetAngle(angle);
	}

	public FreeDirection<TDeltaPointNumber> GetNewDirectionFromRandomTurn()
	{
		return new(new Random().NextDouble() * 360);
	}

	public FreeDirection<TDeltaPointNumber> GetNewDirectionFromTurn(double angleDelta)
	{
		return new(this.Angle + angleDelta);
	}

	public void SetDeltaPoint(Point<TDeltaPointNumber> point)
	{
		this.Value = point;
		var newAngle = this.Value.ToAngle();
		if (!newAngle.Equals(this.Angle)) this.Angle = newAngle;
	}

	public void SetAngle(double angle)
	{
		this.Angle = angle;
		var newDeltaPoint = new Point<TDeltaPointNumber>(this.Angle);
		if (newDeltaPoint != this.Value) this.Value = newDeltaPoint;
	}
}