namespace CodeChops.Geometry.Space;

public static class PointExtensions
{
	public static double ToAngle<TNumber>(this Point<TNumber> point)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		return Math.Atan2(point.X.Convert<double>(), -point.Y.Convert<double>()) * (180 / Math.PI);
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