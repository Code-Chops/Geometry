namespace CodeChops.Geometry.Space.Points;

public static class PointExtensions
{
	public static PointIterator GetEnumerator(this (Point<double> Start, Point<double> End) point)
	{
		return new PointIterator(point.Start, point.End);
	}
	
	public static double ToAngle<TNumber>(this Point<TNumber> point)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		return Math.Atan2(point.X.ConvertToPrimitive<double>(), -point.Y.ConvertToPrimitive<double>()) * (180 / Math.PI);
	}

	public static string InlineCss<TNumber>(this Point<TNumber> point, string unitOfMeasurement = "px")
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		var originalCultureInfo = CultureInfo.CurrentUICulture;
		try
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			var inlineCss = $"left: {point.X.Value}{unitOfMeasurement}; top: {point.Y.Value}{unitOfMeasurement}; ";

			return inlineCss;
		}
		finally
		{
			Thread.CurrentThread.CurrentCulture = originalCultureInfo;
		}
	}
}