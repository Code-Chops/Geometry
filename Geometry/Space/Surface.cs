using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Space.Sizes;

namespace CodeChops.Geometry.Space;

public abstract class Surface<TNumber> : Entity, ISurface 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	private Size<TNumber> Size { get; }
	public Point<TNumber> Offset { get; }
	
	/// <summary>
	/// The sum of the width and height.
	/// </summary>
	public Number<TNumber> Circumference { get; }
	/// <summary>
	/// The multiplication of the width and height. 
	/// </summary>
	public Number<TNumber> Area { get; }

	public Number<TNumber> Width => this.Size.Width;
	public Number<TNumber> Height => this.Size.Height;

	[JsonConstructor]
	public Surface(Size<TNumber> size, Point<TNumber>? offset = null)
	{
		this.Size = size;
		this.Offset = offset ?? Point<TNumber>.Empty;
		this.Circumference = this.Size.GetCircumference();
		this.Area = this.Size.GetArea();
	}

	/// <summary>
	/// Enumerates all points from a starting point in a certain direction of 'length' amount of steps.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">If length is smaller than 0.</exception>
	/// <exception cref="PointOutOfBoundsException{Surface,Point}">If the point exceeds the bounds of the surface.</exception>
	public IEnumerable<Point<TNumber>> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int length)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");
		
		var point = startingPoint - direction.Value; // Go backwards one time because the iteration below will go forward immediately.

		if (this.IsOutOfBounds(startingPoint)) throw PointOutOfBoundsException<Surface<TNumber>, Point<TNumber>>.Create((this, startingPoint));
		
		for (var i = 0; i < length; i++)
			yield return this.IsOutOfBounds(point += direction.Value) 
				? throw PointOutOfBoundsException<Surface<TNumber>, Point<TNumber>>.Create((this, point))
				: point;
	}

	/// <summary>
	/// Enumerates all points of the surface (starting from left to right and then downwards).
	/// </summary>
	public IEnumerable<Point<TNumber>> GetAllPoints()
	{
		foreach (var point in this.Size.GetAllPoints())
			yield return point + this.Offset;
	}

	/// <summary>
	/// Tries to get the address of a point (starting from left to right and then downwards).
	/// </summary>
	public bool TryGetAddress(Point<TNumber> point, [NotNullWhen(returnValue: true)] out Number<TNumber>? address)
	{
		address = (point.Y - this.Offset.Y) * this.Width + point.X - this.Offset.X;
		if (this.IsOutOfBounds(address.Value))
		{
			address = null;
			return false;
		}
		
		return true;
	}
	
	/// <summary>
	/// Checks if the provided point is out of the surface's bounds. 
	/// </summary>
	public bool IsOutOfBounds(Point<TNumber> point) => !this.TryGetAddress(point, out _);
	
	/// <summary>
	/// Checks if the provided address is out of the surface's bounds. 
	/// </summary>
	public bool IsOutOfBounds(TNumber address) => address < Number<TNumber>.Zero || address >= this.Area;
}