namespace CodeChops.Geometry.Space.Points;

public ref struct PointIterator
{
	public Point<double> Current { get; private set; }

	private double Count { get; set; }
	private double Distance { get; }
	private Point<double> Delta { get; }
	
	public PointIterator(Point<double> startingPoint, Point<double> endPoint)
	{
		var difference = (endPoint - startingPoint).Convert<double>();
		var distance = Math.Sqrt(Math.Pow(difference.X, 2) + Math.Pow(difference.Y, 2));
		this.Distance = Math.Round(distance, MidpointRounding.AwayFromZero) - 1;
		this.Delta = difference / (this.Distance - 1);
		this.Current = startingPoint - this.Delta;
	}

	// ReSharper disable once UnusedMember.Global
	public bool MoveNext()
	{
		this.Current += this.Delta;
		this.Count++;
		
		return this.Count <= this.Distance;
	}
}