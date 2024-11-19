using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using CodeChops.Geometry.Space.Directions;
using CodeChops.Geometry.Space.Lines;
using CodeChops.Geometry.Space.Movements;

namespace CodeChops.Geometry.Space.Surface;

/// <summary>
/// An Euclidean plane of a specific size (and offset).
/// </summary>
/// <typeparam name="TNumber"></typeparam>
[StructLayout(LayoutKind.Auto)]
public readonly record struct Surface<TNumber> : ISurface<TNumber>
	where TNumber : struct, INumber<TNumber>
{
	public override string ToString() => this.ToDisplayString(new { this.Size, this.Offset });

	public Size<TNumber> Size { get; }
	public Point<TNumber> Offset { get; }

	/// <summary>
	/// The sum of the width and height.
	/// </summary>
	public TNumber Circumference { get; }
	/// <summary>
	/// The multiplication of the width and height.
	/// </summary>
	public TNumber Area { get; }

	[JsonConstructor]
	public Surface(Size<TNumber> size, Point<TNumber> offset = default)
	{
		this.Size = size;
		this.Offset = offset;
		this.Circumference = this.Size.Circumference();
		this.Area = this.Size.Area();
	}

	/// <summary>
	/// Enumerates all points from a starting point in a certain direction.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">If length is smaller than 0.</exception>
	/// <param name="length">The count of points to take. If 0, it continues until the end of the surface.</param>
	public IEnumerable<Point<TNumber>> GetPointsInDirection(IStraightMovement<TNumber> movement, int length = 0, Validator? validation = null)
		=> GetPointsInDirection(movement.StartingPoint, (IDirection<TNumber>)movement.GetDirection(), length, validation);

	/// <summary>
	/// Enumerates all points from a starting point in a certain direction.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">If length is smaller than 0.</exception>
	/// <param name="length">The count of points to take. If 0, it continues until the end of the surface.</param>
	public IEnumerable<Point<TNumber>> GetPointsInDirection(Point<TNumber> startingPoint, IDirection<TNumber> direction, int length = 0, Validator? validator = null)
	{
		if (length < 0) throw new ArgumentOutOfRangeException($"Length cannot be smaller than 0. Provided length is {length}.");

		validator = length == 0
			? Validator.Get<Surface<TNumber>>.Aggregate()
			: validator ?? Validator.Get<Surface<TNumber>>.Default;

		var point = startingPoint;

		var i = 0;
		while (i < length)
		{
			validator.GuardInRange(this, point, errorCode: null);
			if (validator.HasException)
				break;

			yield return point;

			point += direction.Value;
			i++;
		}
	}

	/// <summary>
	/// Enumerates all points of a line.
	/// </summary>
	public IEnumerable<Point<TNumber>> GetPointsOfLine(Line<TNumber> line, Validator? validator = null)
	{
		validator ??= new DefaultValidator(this.GetType().Name);

		foreach (var point in line)
		{
			validator.GuardInRange(this, point, errorCode: null);

			yield return point;
		}
	}

	/// <summary>
	/// Tries to get the address of a point (starting from left to right and then downwards). Is null when out of bounds.
	/// </summary>
	public TNumber GetAddress(Point<TNumber> point, Validator? validator = null)
		=> (validator ?? Validator.Get<Surface<TNumber>>.Default).GuardInRange(this, point, errorCode: null);

	public Surface<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : struct, INumber<TTargetNumber>, IPowerFunctions<TTargetNumber>, IRootFunctions<TTargetNumber>
		=> new(this.Size.Convert<TTargetNumber>(), this.Offset.Convert<TTargetNumber>());

	public SizeIterator<TNumber> GetEnumerator()
		=> new(this.Size);
}