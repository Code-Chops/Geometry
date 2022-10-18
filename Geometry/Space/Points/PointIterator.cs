namespace CodeChops.Geometry.Space.Points;

public struct PointIterator
{
	public Point<double> Current { get; private set; }
	
	private Point<double> EndPoint { get; }

	private double Count { get; set; }
	private double Length { get; }
	private Point<double> Delta { get; }
	
	public PointIterator(Point<double> startingPoint, Point<double> endPoint)
	{
		this.EndPoint = endPoint;

		var difference = (endPoint - startingPoint).Convert<double>();

		var squaredLength = difference.X * difference.X + difference.Y * difference.Y;
		this.Length = Math.Round(Math.Sqrt(squaredLength), MidpointRounding.AwayFromZero) - 1;
		this.Delta = difference / (this.Length - 1);
		this.Current = startingPoint - this.Delta;
	}

	// ReSharper disable once UnusedMember.Global
	public bool MoveNext()
	{
		this.Current += this.Delta;
		this.Count++;
		
		return this.Count <= this.Length;
	}
}