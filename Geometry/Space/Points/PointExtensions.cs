using CodeChops.Geometry.Space.Directions.Free;

namespace CodeChops.Geometry.Space.Points;

public static class PointExtensions
{
	public static Angle ToAngle<TNumber>(this Point<TNumber> point)
		where TNumber :  INumber<TNumber> 
	{
		var convertedPoint = point.Convert<double>();
		return new(Double.Atan2(convertedPoint.X, -convertedPoint.Y) * (180 / Math.PI));
	}
	
	public static string InlineCss<TNumber>(this Point<TNumber> point, string unitOfMeasurement = "px")
		where TNumber : INumber<TNumber>
	{
		var originalCultureInfo = CultureInfo.CurrentUICulture;

		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		var inlineCss = $"left: {point.X}{unitOfMeasurement}; top: {point.Y}{unitOfMeasurement}; ";
		Thread.CurrentThread.CurrentCulture = originalCultureInfo;
		
		return inlineCss;
	}
}