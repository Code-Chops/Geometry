using CodeChops.GenericMath;
using CodeChops.Geometry.Space;

namespace CodeChops.Geometry.Space;

public static class PointExtensions
{
	public static Point<TNumber> Cast<TSourceNumber, TNumber>(this Point<TSourceNumber> point)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
		where TSourceNumber : struct, IComparable<TSourceNumber>, IEquatable<TSourceNumber>, IConvertible
	{
		return new(
			x: point.X.Cast<TSourceNumber, TNumber>(),
			y: point.Y.Cast<TSourceNumber, TNumber>());
	}

	public static double ToAngle<TNumber>(this Point<TNumber> point)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		return Math.Atan2(point.X.Cast<TNumber, double>(), -point.Y.Cast<TNumber, double>()) * (180 / Math.PI);
	}

	public static string InlineCss<TNumber>(this Point<TNumber> point, string unitOfMeasurement = "px")
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		return $"left: {point.X}{unitOfMeasurement}; top: {point.Y}{unitOfMeasurement}; ";
	}
}