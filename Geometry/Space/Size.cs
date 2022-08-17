using CodeChops.DomainDrivenDesign.DomainModeling.Factories;

namespace CodeChops.Geometry.Space;

/// <summary>
/// A 2-dimensional measurement with TNumber as type of the underlying values of Width and Height.
/// </summary>
public readonly struct Size<TNumber> : IValueObject, IComparable<Size<TNumber>>, IHasEmptyInstance<Size<TNumber>>
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
	
	public override string ToString() => $"({this.Width}, {this.Height})";

	public Number<TNumber> Width { get; init; }
	public Number<TNumber> Height { get; init; }

	public static Size<TNumber> Empty { get; } = new();

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
		this.Width = (TNumber)Number<TNumber>.Create(int.Parse(width, NumberStyles.AllowDecimalPoint));
		this.Height = (TNumber)Number<TNumber>.Create(int.Parse(height, NumberStyles.AllowDecimalPoint));
	}

	public void Deconstruct(out Number<TNumber> width, out Number<TNumber> height)
	{
		width = this.Width;
		height = this.Height;
	}

	public static Size<TNumber> Create<TOriginalNumber>(TOriginalNumber width, TOriginalNumber height)
		where TOriginalNumber : struct, IComparable<TOriginalNumber>, IEquatable<TOriginalNumber>, IConvertible
	{
		return new Size<TNumber>(
			width: (TNumber)Convert.ChangeType(width, typeof(TNumber)),
			height: (TNumber)Convert.ChangeType(height, typeof(TNumber)));
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

	public static explicit operator Size<TNumber>(Point<TNumber> point)
	{
		return new(point.X, point.Y);
	}

	public static implicit operator Size<TNumber>((TNumber, TNumber) tuple)
	{
		return new(tuple.Item1, tuple.Item2);
	}
	
	public Size<TTargetNumber> Cast<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return Size<TTargetNumber>.Create<TNumber>(this.Width, this.Height);
	}

	public static Number<TNumber> Sqrt(Size<TNumber> size)
	{
		var number = size.Width * size.Width + size.Height * size.Height;
		return number * number;
	}

	public Number<TNumber> ToAddress() => this.Height * this.Width;

	public Number<TNumber> Sum() => this.Width + this.Height;
}