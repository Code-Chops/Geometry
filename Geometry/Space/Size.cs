using CodeChops.GenericMath;

namespace CodeChops.Geometry.Space;

/// <summary>
/// A 2-dimensional measurement with TNumber as type of the underlying values of Width and Height.
/// </summary>
public struct Size<TNumber> where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => $"({this.Width}, {this.Height})";

	public static readonly Size<TNumber> Default = new();

	public Number<TNumber> Width { get; set; }
	public Number<TNumber> Height { get; set; }

	public Size(Number<TNumber> width, Number<TNumber> height)
	{
		this.Width = width;
		this.Height = height;
	}

	public Size(Point<TNumber> point)
	{
		this.Width = point.X;
		this.Height = point.Y;
	}

	public Number<TNumber> GetTotalIndex()
	{
		return this.Width * this.Height;
	}

	public Size(string width, string height)
	{
		this.Width = (TNumber)Number<TNumber>.Create(int.Parse(width, System.Globalization.NumberStyles.AllowDecimalPoint));
		this.Height = (TNumber)Number<TNumber>.Create(int.Parse(height, System.Globalization.NumberStyles.AllowDecimalPoint));
	}

	public void Deconstruct(out Number<TNumber> width, out Number<TNumber> height)
	{
		width = this.Width;
		height = this.Height;
	}

	public static Size<TNumber> operator +(Size<TNumber> s1, Size<TNumber> s2)
	{
		return new(s1.Width + s2.Width, s1.Height + s2.Height);
	}

	public static Size<TNumber> operator -(Size<TNumber> s1, Size<TNumber> s2)
	{
		return new(s1.Width - s2.Width, s1.Height - s2.Height);
	}

	public static Size<TNumber> operator *(Size<TNumber> s1, Size<TNumber> s2)
	{
		return new(s1.Width * s2.Width, s1.Height * s2.Height);
	}

	public static Size<TNumber> operator *(Size<TNumber> s1, Number<TNumber> s2)
	{
		return new(s1.Width * s2, s1.Height * s2);
	}

	public static implicit operator Size<TNumber>(Point<TNumber> point)
	{
		return new(point.X, point.Y);
	}

	public static implicit operator Size<TNumber>((TNumber, TNumber) tuple)
	{
		return new(tuple.Item1, tuple.Item2);
	}

	public static Number<TNumber> Sqrt(Size<TNumber> size)
	{
		var number = size.Width * size.Width + size.Height * size.Height;
		return number * number;
	}

	public Number<TNumber> ToAddress() => this.Height * this.Width;

	public Number<TNumber> Sum() => this.Width + this.Height;
}