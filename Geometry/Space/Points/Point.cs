using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Sizes;

namespace CodeChops.Geometry.Space.Points;

/// <summary>
/// A 2-dimensional location with TNumber als type of the underlying values of X and Y. 
/// </summary>
public readonly record struct Point<TNumber> : IPoint, IComparable<Point<TNumber>>, IHasDefaultInstance<Point<TNumber>> 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	#region Comparison
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Point<TNumber> other)
	{
		if (this.X == other.X) return (this.Y - other.Y).Value.ToInt32(CultureInfo.InvariantCulture);
		return (this.X - other.X).Value.ToInt32(CultureInfo.InvariantCulture);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <(Point<TNumber> left, Point<TNumber> right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=(Point<TNumber> left, Point<TNumber> right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >(Point<TNumber> left, Point<TNumber> right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=(Point<TNumber> left, Point<TNumber> right)	=> left.CompareTo(right) >= 0;
	
	#endregion
	
	public override string ToString() => $"({this.X}, {this.Y})";

	public Number<TNumber> X { get; }
	public Number<TNumber> Y { get; }

	/// <summary>
	/// (0, 0)
	/// </summary>
	public static Point<TNumber> DefaultInstance { get; } = new();

	/// <summary>
	/// Sums up X and Y.
	/// </summary>
	public Number<TNumber> Sum() => Calculator<TNumber>.Add(this.X, this.Y);
	
	/// <summary>
	/// Multiplies X and Y.
	/// </summary>
	public Number<TNumber> Multiply() => Calculator<TNumber>.Multiply(this.X, this.Y);
	
	[JsonConstructor]
	public Point(Number<TNumber> x, Number<TNumber> y)
	{
		this.X = x;
		this.Y = y;
	}

	public Point(Number<TNumber> address, Size<TNumber> size)
	{
		this.X = address % size.Width;
		this.Y = address / size.Width;
	}

	public Point(double angle)
	{
		this.X = (TNumber)System.Convert.ChangeType(Math.Cos((angle - 90) / 180 * Math.PI), typeof(TNumber));
		this.Y = (TNumber)System.Convert.ChangeType(Math.Sin((angle - 90) / 180 * Math.PI), typeof(TNumber));
	}
	
	public void Deconstruct(out Number<TNumber> x, out Number<TNumber> y)
	{
		x = this.X;
		y = this.Y;
	}

	public static Point<TNumber> operator +(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X + point2.X, point1.Y + point2.Y);
	
	public static Point<TNumber> operator +(Point<TNumber> point, Number<TNumber> number) 
		=> new(point.X + number, point.Y + number);

	public static Point<TNumber> operator -(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X - point2.X, point1.Y - point2.Y);

	public static Point<TNumber> operator -(Point<TNumber> point, Number<TNumber> number) 
		=> new(point.X - number, point.Y - number);

	public static Point<TNumber> operator *(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X * point2.X, point1.Y * point2.Y);

	public static Point<TNumber> operator *(Point<TNumber> point, Number<TNumber> factor) 
		=> new(point.X * factor, point.Y * factor);
	
	public static Point<TNumber> operator /(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X / point2.X, point1.Y / point2.Y);

	public static Point<TNumber> operator /(Point<TNumber> point, Number<TNumber> factor) 
		=> new(point.X / factor, point.Y / factor);

	public static explicit operator Point<TNumber>(Size<TNumber> size) 
		=> new(size.Width, size.Height);

	public static implicit operator Point<TNumber>((Number<TNumber>, Number<TNumber>) tuple) 
		=> new(tuple.Item1, tuple.Item2);
	
	public static implicit operator Point<TNumber>((TNumber, TNumber) tuple) 
		=> new(tuple.Item1, tuple.Item2);

	public Point<TTarget> Convert<TTarget>()
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
	{
		return new Point<TTarget>(this.X.ConvertToPrimitive<TTarget>(), this.Y.ConvertToPrimitive<TTarget>());
	}
}