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

	public IEnumerable<(int Index, Point<TNumber>)> GetAllPointsInSize()
	{
		var index = 0;
		for (var y = new Number<TNumber>(); y < this.Height; y++)
			for (var x = new Number<TNumber>(); x < this.Width; x++)
				yield return (index++, new Point<TNumber>(x, y));
	}

	public Number<TNumber> Width { get; init; }
	public Number<TNumber> Height { get; init; }
	

	public static Size<TNumber> Empty { get; } = new();

	public ulong Count() => this.Height.Value.ToUInt64(CultureInfo.InvariantCulture) * this.Width.Value.ToUInt64(CultureInfo.InvariantCulture);
	public ulong Sum() => this.Width.Value.ToUInt64(CultureInfo.InvariantCulture) + this.Height.Value.ToUInt64(CultureInfo.InvariantCulture);

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
	
	public static Size<TNumber> operator +(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width + size2.Width, size1.Height + size2.Height);

	public static Size<TNumber> operator -(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width - size2.Width, size1.Height - size2.Height);

	public static Size<TNumber> operator *(Size<TNumber> size, Size<TNumber> size2) 
		=> new(size.Width * size2.Width, size.Height * size2.Height);

	public static Size<TNumber> operator *(Size<TNumber> size, Number<TNumber> factor) 
		=> new(size.Width * factor, size.Height * factor);

	public static Size<TNumber> operator /(Size<TNumber> size1, Size<TNumber> size2) 
		=> new(size1.Width / size2.Width, size1.Height / size2.Height);

	public static Size<TNumber> operator /(Size<TNumber> size, TNumber factor) 
		=> new(size.Width / factor, size.Height / factor);

	public static explicit operator Size<TNumber>(Point<TNumber> point) 
		=> new(point.X, point.Y);

	public static implicit operator Size<TNumber>((TNumber, TNumber) tuple) 
		=> new(tuple.Item1, tuple.Item2);

	public Size<TTargetNumber> Cast<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return Size<TTargetNumber>.Create<TNumber>(this.Width, this.Height);
	}

	public bool TryGetAddress(Point<TNumber> point, [NotNullWhen(returnValue: true)] out ulong? address)
	{
		if (point.X < Number<TNumber>.Empty || point.X >= this.Width)
		{
			address = null;
			return false;
		}

		var addressNumber = point.Y * this.Width + point.X;
		address = addressNumber.Value.ToUInt64(CultureInfo.InvariantCulture);
		return !this.IsOutOfRange(address.Value);
	}

	public bool IsOutOfRange(ulong address) => address >= this.Count();
}