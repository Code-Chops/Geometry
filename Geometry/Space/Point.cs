using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space;

/// <summary>
/// A 2-dimensional location with TNumber als type of the underlying values of X and Y. 
/// </summary>
public readonly record struct Point<TNumber> : IValueObject, IComparable<Point<TNumber>>, IHasEmptyInstance<Point<TNumber>>
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

	public IEnumerable<(int Index, Point<TNumber>)> GetPointsInDirection(IDirection<TNumber> direction, int length)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");
		
		var index = 0;
		var point = this - direction.Value;
		for (var i = 0; i < length; i++)
			yield return (index++, point += direction.Value);
	}
	
	public Number<TNumber> X { get; init; }
	public Number<TNumber> Y { get; init; }

	public static Point<TNumber> Empty { get; } = new();

	public int Count() => this.X.ConvertToPrimitive<int>() * this.Y.ConvertToPrimitive<int>();
	public int Sum() => this.X.ConvertToPrimitive<int>() + this.Y.ConvertToPrimitive<int>();

	public Point(TNumber x, TNumber y)
	{
		this.X = x;
		this.Y = y;
	}

	public Point(double angle)
	{
		this.X = (TNumber)System.Convert.ChangeType(Math.Cos((angle - 90) / 180 * Math.PI), typeof(TNumber));
		this.Y = (TNumber)System.Convert.ChangeType(Math.Sin((angle - 90) / 180 * Math.PI), typeof(TNumber));
	}

	public Point(Number<TNumber> address, Size<TNumber> size)
	{
		this.X = address % size.Width;
		this.Y = address / size.Width;
	}
	
	public void Deconstruct(out Number<TNumber> x, out Number<TNumber> y)
	{
		x = this.X;
		y = this.Y;
	}

	public static Point<TNumber> operator +(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X + point2.X, point1.Y + point2.Y);

	public static Point<TNumber> operator -(Point<TNumber> point1, Point<TNumber> point2) 
		=> new(point1.X - point2.X, point1.Y - point2.Y);

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

	public bool TryGetAddress(Size<TNumber> size, [NotNullWhen(returnValue: true)] out int? address)
	{
		if (this.X < Number<TNumber>.Zero || this.X >= size.Width)
		{
			address = null;
			return false;
		}

		var addressNumber = this.Y * size.Width + this.X;
		address = addressNumber.Value.ToInt32(CultureInfo.InvariantCulture);
		return !size.IsOutOfRange(address.Value);
	}
	
	public bool IsOutOfRange(Size<TNumber> size) => !this.TryGetAddress(size, out _);
}