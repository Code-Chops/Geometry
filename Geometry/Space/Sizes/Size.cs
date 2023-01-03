using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace CodeChops.Geometry.Space.Sizes;

/// <summary>
/// A 2-dimensional measurement with TNumber as type of the underlying values of Width and Height.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Size<TNumber> : ISize, IComparable<Size<TNumber>>, IHasDefault<Size<TNumber>>
	where TNumber : INumber<TNumber>
{
	#region Comparison
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Size<TNumber> other)
	{
		if (this.Height < other.Height || this.Width < other.Width)
			return -1;
		if (this.Height > other.Height || this.Width > other.Width) 
			return 1;

		return 0;
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

	public TNumber Width { get; }
	public TNumber Height { get; }
	
	/// <summary>
	/// (0, 0)
	/// </summary>
	public static Size<TNumber> Default { get; } = new(TNumber.Zero, TNumber.Zero);
	
	/// <summary>
	/// Sums up the width and height to get the circumference.
	/// </summary>
	public TNumber Circumference() => this.Width + this.Height;
	
	/// <summary>
	/// Multiplies the width and height to get the area.
	/// </summary>
	public TNumber Area() => this.Height * this.Width;
	
	public Size(Point<TNumber> point)
		: this(point.X, point.Y)
	{
	}
	
	[JsonConstructor]
	public Size(TNumber width, TNumber height)
	{
		this.Width = width;
		this.Height = height;
	}

	public static Size<TNumber> Create<TSourceNumber>(TSourceNumber width, TSourceNumber height)
		where TSourceNumber : INumber<TSourceNumber>
	{
		return new Size<TNumber>(TNumber.CreateChecked(width), TNumber.CreateChecked(height));
	}
	
	[Obsolete("Don't use this empty constructor. A value should be provided when initializing Size<TNumber>.", error: true)]
	public Size() => throw new InvalidOperationException($"Don't use this empty constructor. A value should be provided when initializing Size<TNumber>.");
	
	public void Deconstruct(out TNumber width, out TNumber height)
	{
		width = this.Width;
		height = this.Height;
	}

	internal TNumber GetAddress(Point<TNumber> point, Point<TNumber> offset)
	{
		return (point.Y - offset.Y) * this.Width + point.X - offset.X;
	}

	/// <summary>
	/// Enumerates all points of the surface starting from left to right and then downwards.
	/// </summary>
	public IEnumerable<Point<TNumber>> GetAllPoints()
	{
		for (var y = TNumber.Zero; y < this.Height; y++)
			for (var x = TNumber.Zero; x < this.Width; x++)
				yield return new(x, y);
	}
	
	public static Size<TNumber> operator +(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width + size2.Width, size1.Height + size2.Height);
	
	public static Size<TNumber> operator +(Size<TNumber> size, TNumber number) 
		=> new(size.Width + number, size.Height + number);

	public static Size<TNumber> operator -(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width - size2.Width, size1.Height - size2.Height);

	public static Size<TNumber> operator -(Size<TNumber> size, TNumber number) 
		=> new(size.Width - number, size.Height - number);
	
	public static Size<TNumber> operator *(Size<TNumber> size, Size<TNumber> size2) 
		=> new(size.Width * size2.Width, size.Height * size2.Height);

	public static Size<TNumber> operator *(Size<TNumber> size, TNumber factor) 
		=> new(size.Width * factor, size.Height * factor);

	public static Size<TNumber> operator /(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width / size2.Width, size1.Height / size2.Height);

	public static explicit operator Size<TNumber>(Point<TNumber> point) 
		=> new(point.X, point.Y);

	public static implicit operator Size<TNumber>((TNumber, TNumber) tuple) 
		=> new(tuple.Item1, tuple.Item2);

	public Size<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>
    {
	    return Size<TTargetNumber>.Create(this.Width, this.Height);
	}
	
	public SizeIterator<TNumber> GetEnumerator()
		=> new(this);
}