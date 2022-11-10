using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space;

public abstract class Surface<TSelf, TNumber> : Entity, ISurface<TNumber>
	where TSelf : Surface<TSelf, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public override string ToString() => this.ToDisplayString(new { this.Size, this.Offset });
	
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
	protected Surface(Size<TNumber> size, Point<TNumber>? offset = null)
	{
		this.Size = size;
		this.Offset = offset ?? Point<TNumber>.DefaultInstance;
		this.Circumference = this.Size.GetCircumference();
		this.Area = this.Size.GetArea();
	}

	/// <summary>
	/// Enumerates all points from a starting point in a certain direction.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">If length is smaller than 0.</exception>
	/// <param name="length">The amount of steps to take. If null, it continues until the end of the surface.</param>
	public IEnumerable<Point<TNumber>> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int? length = null, Validator? validator = null)
	{
		validator ??= Validator.Get<TSelf>.Default;
		
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");

		var point = startingPoint;

		var i = 0;
		while (length is null || i < length)
		{
			validator.GuardInRange(this, point, errorCode: null);
			
			yield return point;
			
			point += direction.Value;
			i++;
		}
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
		
		if (Validator.Get<TSelf>.Oblivious.GuardInRange(this, address.Value, errorCode: null))
		{
			address = null;
			return false;
		}
		
		return true;
	}
}