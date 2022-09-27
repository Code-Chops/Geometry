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
	
	/// <summary>
	/// The sum of the width and height.
	/// </summary>
	public Number<TNumber> Circumference { get; }
	/// <summary>
	/// The multiplication of the width and height. 
	/// </summary>
	public Number<TNumber> Area { get; }

	[JsonConstructor]
	public Surface(Size<TNumber> size, Point<TNumber>? offset = null)
	{
		this.Size = size;
		this.Offset = offset ?? Point<TNumber>.Empty;
		this.Circumference = this.Size.GetCircumference();
		this.Area = this.Size.GetArea();
	}

	/// <summary>
	/// Enumerates all points from a starting point in a certain direction.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">If length is smaller than 0.</exception>
	/// <exception cref="PointOutOfBoundsException{Surface,Point}">If the point exceeds the bounds of the surface (when length is provided).</exception>
	/// <param name="length">The amount of steps to take. If null, it continues until the end of the surface.</param>
	public IEnumerable<Point<TNumber>> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int? length = null)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");

		var point = startingPoint; // Go backwards one time because the iteration below will go forward immediately.

		var i = 0;
		while (length is null || i < length)
		{
			if (this.IsOutOfBounds(point)) break;
			
			yield return point;
			
			point += direction.Value;
			i++;
		}

		// If length is not null and loop is terminated prematurely.
		if (i < length) new PointOutOfBoundsException<Surface<TNumber>, Point<TNumber>>().Throw(point);
	}

	/// <summary>
	/// Enumerates all points of the surface (starting from left to right and then downwards).
	/// </summary>
	public IEnumerable<Point<TNumber>> GetAllPoints() 
		=> this.Size.GetAllPoints().Select(point => point + this.Offset);

	/// <summary>
	/// Tries to get the address of a point (starting from left to right and then downwards). Is null when out of bounds.
	/// </summary>
	/// <returns>False when out of bounds.</returns>
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
	
	/// <summary>
	/// Checks if the provided point is out of the surface's bounds. 
	/// </summary>
	public bool IsOutOfBounds(Point<TNumber> point)
	{
		point -= this.Offset;
		return point.X < Number<TNumber>.Zero || point.Y < Number<TNumber>.Zero || point.X >= this.Size.Width || point.Y >= this.Size.Height;
	}

	/// <summary>
	/// Checks if the provided address is out of the surface's bounds. 
	/// </summary>
	public bool IsOutOfBounds(TNumber address) => address < Number<TNumber>.Zero || address >= this.Area;
}