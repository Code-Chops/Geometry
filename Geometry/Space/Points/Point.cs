using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace CodeChops.Geometry.Space.Points;

/// <summary>
/// A 2-dimensional location with TNumber as type of the underlying values of X and Y. 
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Point<TNumber> : IPoint<TNumber>, IHasDefault<Point<TNumber>>, IComparable<Point<TNumber>>
	where TNumber : INumber<TNumber>, IAdditionOperators<TNumber, TNumber, TNumber>
{
	public override string ToString() => $"({this.X}, {this.Y})";
	
	#region Comparison
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Point<TNumber> other)
	{
		if (this.Y < other.Y || this.X < other.X)
			return -1;
		if (this.Y > other.Y || this.X > other.X) 
			return 1;

		return 0;
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
	
	public TNumber X { get; }
	public TNumber Y { get; }

	/// <summary>
	/// (0, 0)
	/// </summary>
	public static Point<TNumber> Default { get; } = new(TNumber.Zero, TNumber.Zero);

	/// <summary>
	/// Sums up X and Y.
	/// </summary>
	public TNumber Sum() => this.X + this.Y;
	
	/// <summary>
	/// Multiplies X and Y.
	/// </summary>
	public TNumber Multiply() => this.X * this.Y;

	[JsonConstructor]
	public Point(TNumber x, TNumber y)
	{
		this.X = x;
		this.Y = y;
	}

	public Point(TNumber address, Size<TNumber> size)
	{
		this.X = address % size.Width;
		this.Y = address / size.Width;
	}

	public Point(double angle)
	{
		this.X = TNumber.CreateChecked(Math.Cos((angle - 90) / 180 * Math.PI));
		this.Y = TNumber.CreateChecked(Math.Sin((angle - 90) / 180 * Math.PI));
	}

	public static Point<TNumber> Create<TSourceNumber>(TSourceNumber x, TSourceNumber y)
		where TSourceNumber : INumber<TSourceNumber>
	{
		return new Point<TNumber>(TNumber.CreateChecked(x), TNumber.CreateChecked(y));
	}
	
	public void Deconstruct(out TNumber x, out TNumber y)
	{
		x = this.X;
		y = this.Y;
	}
	
	[Obsolete("Don't use this empty constructor. A value should be provided when initializing Point<TNumber>.", error: true)]
	public Point() => throw new InvalidOperationException($"Don't use this empty constructor. A value should be provided when initializing Point<TNumber>.");

	public static Point<TNumber> operator +(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X + point2.X, point1.Y + point2.Y);
	
	public static Point<TNumber> operator +(Point<TNumber> point, TNumber number) 
		=> new(point.X + number, point.Y + number);

	public static Point<TNumber> operator -(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X - point2.X, point1.Y - point2.Y);

	public static Point<TNumber> operator -(Point<TNumber> point, TNumber number) 
		=> new(point.X - number, point.Y - number);

	public static Point<TNumber> operator *(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X * point2.X, point1.Y * point2.Y);

	public static Point<TNumber> operator *(Point<TNumber> point, TNumber factor) 
		=> new(point.X * factor, point.Y * factor);
	
	public static Point<TNumber> operator /(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X / point2.X, point1.Y / point2.Y);

	public static Point<TNumber> operator /(Point<TNumber> point, TNumber factor) 
		=> new(point.X / factor, point.Y / factor);

	public static explicit operator Point<TNumber>(Size<TNumber> size) 
		=> new(size.Width, size.Height);

	public static implicit operator Point<TNumber>((TNumber, TNumber) tuple) 
		=> new(tuple.Item1, tuple.Item2);
	
	public Point<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>
	{
		return Point<TTargetNumber>.Create(this.X, this.Y);
	}
}