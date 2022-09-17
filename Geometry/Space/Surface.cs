using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space;

public abstract partial class Surface<TNumber> : Entity
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public Size<TNumber> Size { get; init; }
	public Point<TNumber> Offset { get; init; }

	public Surface(Size<TNumber> size, Point<TNumber>? offset = null)
	{
		this.Size = size;
		this.Offset = offset ?? Point<TNumber>.Empty;
	}

	public IEnumerable<(int Index, Point<TNumber>)> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int length)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");
		
		var index = 0;
		var point = startingPoint - direction.Value;
		
		for (var i = 0; i < length; i++)
			yield return (index++, point += direction.Value);
	}

	public IEnumerable<(int Index, Point<TNumber>)> GetAllPoints()
	{
		var index = 0;
		
		for (var y = new Number<TNumber>(); y < this.Size.Height; y++)
			for (var x = new Number<TNumber>(); x < this.Size.Width; x++)
				yield return (index++, new Point<TNumber>(x, y));
	}

	public bool TryGetAddress(Point<TNumber> point, [NotNullWhen(returnValue: true)] out int? address)
	{
		if (point.X < Number<TNumber>.Zero || point.X >= this.Size.Width)
		{
			address = null;
			return false;
		}

		var addressNumber = point.Y * this.Size.Width + point.X;
		address = addressNumber.Value.ToInt32(CultureInfo.InvariantCulture);
		return !this.IsOutOfRange(address.Value);
	}
	
	public bool IsOutOfRange(Point<TNumber> point) => !this.TryGetAddress(point, out _);
	
	public bool IsOutOfRange(int address) => address < 0 || address >= this.Size.Count;
}