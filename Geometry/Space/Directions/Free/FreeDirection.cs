namespace CodeChops.Geometry.Space.Directions.Free;

/// <summary>
/// A direction with every possible delta point.
/// </summary>
public record FreeDirection<TDeltaPointNumber> : IDirection
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	public override string ToString() => $"{nameof(FreeDirection<TDeltaPointNumber>)}: {this.DeltaPoint}";

	public Point<TDeltaPointNumber> DeltaPoint => this._deltaPoint;
	public Point<float> GetValue() => this.DeltaPoint.Cast<TDeltaPointNumber, float>();

	/// <summary>
	/// Backing field so the value is mutable in this class.
	/// </summary>
	private Point<TDeltaPointNumber> _deltaPoint;

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
		this._deltaPoint = point;
		var newAngle = this._deltaPoint.ToAngle();
		if (!newAngle.Equals(this.Angle)) this.Angle = newAngle;
	}

	public void SetAngle(double angle)
	{
		this.Angle = angle;
		var newDeltaPoint = new Point<TDeltaPointNumber>(this.Angle);
		if (newDeltaPoint != this.DeltaPoint) this._deltaPoint = newDeltaPoint;
	}
}