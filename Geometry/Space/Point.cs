using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using CodeChops.GenericMath;

namespace CodeChops.Geometry.Space;

/// <summary>
/// A 2-dimensional location with TNumber als type of the underlying values of X and Y. 
/// </summary>
public record struct Point<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"(X:{this.X}, Y:{this.Y})";

	public static readonly Point<TNumber> Default = new();

	public Number<TNumber> X { get; set; }
	public Number<TNumber> Y { get; set; }

	public static readonly Point<TNumber> Empty = new();

	[JsonConstructor]
	public Point(Number<TNumber> x, Number<TNumber> y)
	{
		this.X = x;
		this.Y = y;
	}

	public static Point<TNumber> Create<TOriginalNumber>(TOriginalNumber x, TOriginalNumber y)
		where TOriginalNumber : struct, IComparable<TOriginalNumber>, IEquatable<TOriginalNumber>, IConvertible
	{
		return new Point<TNumber>(
			x: (TNumber)Convert.ChangeType(x, typeof(TNumber)),
			y: (TNumber)Convert.ChangeType(y, typeof(TNumber)));
	}

	public Point(double angle)
	{
		this.X = (TNumber)Convert.ChangeType(Math.Cos((angle - 90) / 180 * Math.PI), typeof(TNumber));
		this.Y = (TNumber)Convert.ChangeType(Math.Sin((angle - 90) / 180 * Math.PI), typeof(TNumber));
	}

	public Point(Number<TNumber> address, Size<TNumber> size)
	{
		this.X = Calculator<TNumber>.Modulo(address, size.Width);
		this.Y = address / size.Width;
	}

	public void Deconstruct(out Number<TNumber> x, out Number<TNumber> y)
	{
		x = this.X;
		y = this.Y;
	}

	public static Point<TNumber> operator +(Point<TNumber> p1, Point<TNumber> p2)
	{
		return new(p1.X + p2.X, p1.Y + p2.Y);
	}

	public static Point<TNumber> operator -(Point<TNumber> p1, Point<TNumber> p2)
	{
		return new(p1.X - p2.X, p1.Y - p2.Y);
	}

	public static Point<TNumber> operator +(Point<TNumber> point, Number<TNumber> addition)
	{
		return new(point.X + addition, point.Y + addition);
	}

	public static Point<TNumber> operator -(Point<TNumber> point, Number<TNumber> addition)
	{
		return new(point.X - addition, point.Y - addition);
	}

	public static Point<TNumber> operator *(Point<TNumber> point, Number<TNumber> factor)
	{
		return new(point.X * factor, point.Y * factor);
	}

	public static Point<TNumber> operator *(Point<TNumber> point1, Point<TNumber> point2)
	{
		return new(point1.X * point2.X, point1.Y * point2.Y);
	}

	public static Point<TNumber> operator *(Point<TNumber> point, Size<TNumber> size)
	{
		return new(point.X * size.Width, point.Y * size.Height);
	}

	public static Point<TNumber> operator /(Point<TNumber> point1, Point<TNumber> point2)
	{
		return new(point1.X / point2.X, point1.Y / point2.Y);
	}

	public static Point<TNumber> operator /(Point<TNumber> point, TNumber factor)
	{
		return new(point.X / factor, point.Y / factor);
	}

	public static Point<TNumber> operator /(Point<TNumber> point, Size<TNumber> size)
	{
		return new(point.X / size.Width, point.Y / size.Height);
	}

	public static implicit operator Point<TNumber>(Size<TNumber> size)
	{
		return new(size.Width, size.Height);
	}

	public static implicit operator Point<TNumber>((TNumber, TNumber) tuple)
	{
		return new(tuple.Item1, tuple.Item2);
	}

	public Point<TTargetNumber> Cast<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return Point<TTargetNumber>.Create<TNumber>(this.X, this.Y);
	}

	public bool TryGetAddress(Size<TNumber> size, [NotNullWhen(returnValue: true)] out Number<TNumber>? address)
	{
		if (this.X < Number<TNumber>.Empty || this.X >= size.Width)
		{
			address = null;
			return false;
		}

		address = this.Y * size.Width + this.X;

		return !IsOutOfRange(address.Value, size);
	}

	public Number<TNumber> Sum() => this.X + this.Y;

	public bool IsOutOfRange(Size<TNumber> size) => !this.TryGetAddress(size, out _);

	public static bool IsOutOfRange(Number<TNumber> address, Size<TNumber> size) => address <Number<TNumber>.Empty || address >= size.ToAddress();
}