using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Space.Sizes;

namespace CodeChops.Geometry.Space;

public abstract class Surface<TNumber> : Entity, ISurface 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public Size<TNumber> Size { get; }
	public Point<TNumber> Offset { get; }

	[JsonConstructor]
	public Surface(Size<TNumber> size, Point<TNumber>? offset = null)
	{
		this.Size = size;
		this.Offset = offset ?? Point<TNumber>.Empty;
	}

	public IEnumerable<(int Index, Point<TNumber>)> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int length)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");
		
		var index = 0;
		var point = startingPoint - direction.Value; // Go backwards one time because the iteration below will go forward immediately.

		if (this.IsOutOfBounds(startingPoint)) throw PointOutOfBoundsException<Surface<TNumber>, Point<TNumber>>.Create((this, startingPoint));
		
		for (var i = 0; i < length; i++)
			yield return this.IsOutOfBounds(point += direction.Value) 
				? throw PointOutOfBoundsException<Surface<TNumber>, Point<TNumber>>.Create((this, point))
				: (index++, point);
	}

	public IEnumerable<(int Index, Point<TNumber>)> GetAllPoints()
	{
		var index = 0;
		
		for (var y = this.Offset.Y; y < this.Size.Height + this.Offset.Y; y++)
			for (var x = this.Offset.X; x < this.Size.Width + this.Offset.Y; x++)
				yield return (index++, new Point<TNumber>(x, y));
	}

	public bool TryGetAddress(Point<TNumber> point, [NotNullWhen(returnValue: true)] out Number<TNumber>? address)
	{
		address = (point.Y - this.Offset.Y) * this.Size.Width + point.X - this.Offset.X;
		if (this.IsOutOfBounds(address.Value))
		{
			address = null;
			return false;
		}
		
		return true;
	}
	
	public bool IsOutOfBounds(Point<TNumber> point) => !this.TryGetAddress(point, out _);
	
	public bool IsOutOfBounds(TNumber address) => address < Number<TNumber>.Zero || address >= this.Size.Count;
}