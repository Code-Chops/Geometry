using System.Text.Json.Serialization;

namespace CodeChops.Geometry.Space.Sizes;

/// <summary>
/// A 2-dimensional measurement with TNumber as type of the underlying values of Width and Height.
/// </summary>
public readonly record struct Size<TNumber> : ISize, IComparable<Size<TNumber>>, IHasDefault<Size<TNumber>>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	#region Comparison
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Size<TNumber> other)
	{
		if (this.Width == other.Width) return (this.Height - other.Height).Value.ToInt32(CultureInfo.InvariantCulture);
		return (this.Width - other.Width).Value.ToInt32(CultureInfo.InvariantCulture);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <(Size<TNumber> left, Size<TNumber> right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=(Size<TNumber> left, Size<TNumber> right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >(Size<TNumber> left, Size<TNumber> right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=(Size<TNumber> left, Size<TNumber> right)	=> left.CompareTo(right) >= 0;
	
	#endregion

	public override string ToString() => $"({this.Width} x {this.Height})";

	public Number<TNumber> Width { get; }
	public Number<TNumber> Height { get; }
	
	/// <summary>
	/// (0, 0)
	/// </summary>
	public static Size<TNumber> Default { get; } = new();
	
	/// <summary>
	/// Sums up the width and height to get the circumference.
	/// </summary>
	public Number<TNumber> GetCircumference() => Calculator<TNumber>.Add(this.Width, this.Height);
	
	/// <summary>
	/// Multiplies the width and height to get the area.
	/// </summary>
	public Number<TNumber> GetArea() => Calculator<TNumber>.Multiply(this.Height, this.Width);
	
	public Size(Point<TNumber> point)
		: this(point.X, point.Y)
	{
	}
	
	[JsonConstructor]
	public Size(Number<TNumber> width, Number<TNumber> height)
	{
		this.Width = width;
		this.Height = height;
	}

	public void Deconstruct(out Number<TNumber> width, out Number<TNumber> height)
	{
		width = this.Width;
		height = this.Height;
	}

	/// <summary>
	/// Enumerates all points of the surface starting from left to right and then downwards.
	/// </summary>
	public IEnumerable<Point<TNumber>> GetAllPoints()
	{
		for (var y = Number<TNumber>.Zero; y < this.Height; y++)
			for (var x = Number<TNumber>.Zero; x < this.Width; x++)
				yield return new(x, y);
	}
	
	public static Size<TNumber> operator +(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width + size2.Width, size1.Height + size2.Height);
	
	public static Size<TNumber> operator +(Size<TNumber> size, Number<TNumber> number) 
		=> new(size.Width + number, size.Height + number);

	public static Size<TNumber> operator -(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width - size2.Width, size1.Height - size2.Height);

	public static Size<TNumber> operator -(Size<TNumber> size, Number<TNumber> number) 
		=> new(size.Width - number, size.Height - number);
	
	public static Size<TNumber> operator *(Size<TNumber> size, Size<TNumber> size2) 
		=> new(size.Width * size2.Width, size.Height * size2.Height);

	public static Size<TNumber> operator *(Size<TNumber> size, Number<TNumber> factor) 
		=> new(size.Width * factor, size.Height * factor);

	public static Size<TNumber> operator /(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width / size2.Width, size1.Height / size2.Height);

	public static explicit operator Size<TNumber>(Point<TNumber> point) 
		=> new(point.X, point.Y);

	public static implicit operator Size<TNumber>((Number<TNumber>, Number<TNumber>) tuple) 
		=> new(tuple.Item1, tuple.Item2);

	public static implicit operator Size<TNumber>((TNumber, TNumber) tuple) 
		=> new(tuple.Item1, tuple.Item2);
	
	public Size<TTarget> Convert<TTarget>()
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
	{
		return new Size<TTarget>(this.Width.ConvertToPrimitive<TTarget>(), this.Height.ConvertToPrimitive<TTarget>());
	}
}