using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Directions.Strict;
using CodeChops.Geometry.Space.Movements;

namespace CodeChops.Geometry.Space.Lines;

/// <summary>
/// A line holds two points: a starting point and end point.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Line<TNumber> : ILine<TNumber>
	where TNumber : INumber<TNumber>
{
	public override string ToString() => $"({this.StartingPoint}, {this.Endpoint})";
	
	#region Comparison
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Line<TNumber> other)
	{
		if (this.Endpoint < other.Endpoint || this.StartingPoint < other.StartingPoint)
			return -1;
		if (this.Endpoint > other.Endpoint || this.StartingPoint > other.StartingPoint) 
			return 1;

		return 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <(Line<TNumber> left, Line<TNumber> right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=(Line<TNumber> left, Line<TNumber> right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >(Line<TNumber> left, Line<TNumber> right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=(Line<TNumber> left, Line<TNumber> right)	=> left.CompareTo(right) >= 0;
	
	#endregion
	
	public static explicit operator Size<TNumber>(Line<TNumber> value) => new(value.Endpoint - value.StartingPoint);
	public static explicit operator Line<TNumber>(Size<TNumber> value) => new(startingPoint: Point<TNumber>.Default, endpoint: (Point<TNumber>)value);
	
	public Point<TNumber> StartingPoint { get; }
	public Point<TNumber> Endpoint { get; }

	public static Line<TNumber> Create<TSourceNumber>(Point<TSourceNumber> startingPoint, Point<TSourceNumber> endPoint)
		where TSourceNumber : INumber<TSourceNumber>
	{
		return new Line<TNumber>(startingPoint.Convert<TNumber>(), endPoint.Convert<TNumber>());
	}

	[JsonConstructor]
	public Line(Point<TNumber> startingPoint, Point<TNumber> endpoint)
	{
		this.StartingPoint = startingPoint;
		this.Endpoint = endpoint;
	}
	
	[JsonConstructor]
	public Line(IStraightMovement<TNumber> movement, TNumber length)
	{
		this.StartingPoint = movement.StartingPoint;
		this.Endpoint = movement.StartingPoint + movement.GetDirection().GetDeltaPoint<TNumber>() * length;
	}
	
	[JsonConstructor]
	public Line(Point<TNumber> startingPoint, IStrictDirection<TNumber> direction, TNumber length)
	{
		this.StartingPoint = startingPoint;
		this.Endpoint = startingPoint + direction.Value * length;
	}

	public double Distance()
	{
		var difference = (this.Endpoint - this.StartingPoint).Convert<double>();

		return Math.Sqrt(Math.Pow(difference.X, 2) + Math.Pow(difference.Y, 2));
	}

	public Point<TNumber> Difference()
	{
		return this.Endpoint - this.StartingPoint;
	}
	
	public void Deconstruct(out Point<TNumber> width, out Point<TNumber> height)
	{
		width = this.StartingPoint;
		height = this.Endpoint;
	}
	
	[Obsolete("Don't use this empty constructor. A value should be provided when initializing Line<TNumber>.", error: true)]
	public Line() => throw new InvalidOperationException($"Don't use this empty constructor. A value should be provided when initializing Line<TNumber>.");
	
	public Line<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>, IPowerFunctions<TTargetNumber>, IRootFunctions<TTargetNumber>
	{
		return Line<TTargetNumber>.Create(this.StartingPoint, this.Endpoint);
	}
	
	public LineIterator<TNumber> GetEnumerator()
		=> new(this);
}