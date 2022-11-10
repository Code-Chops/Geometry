namespace CodeChops.Geometry.Space;

public static class InRangeGuardExtensions
{
	/// <summary>
	/// Checks if the provided address is in the surface's bounds. 
	/// </summary>
	public static bool GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, Number<TNumber> value, 
		IErrorCode? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		var parameters = (value, Number<TNumber>.Zero, surface.Area);

		InRangeNoOutputGuard<TNumber>.Guard(validator, parameters, parameters, errorCode, innerException);

		return validator.IsValid;
	}
	
	/// <summary>
	/// Checks if the provided point is in the surface's bounds. 
	/// </summary>
	public static Point<TNumber> GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, Point<TNumber> point, 
		IErrorCode? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		point -= surface.Offset;
		var parametersX = (value: point.X, Number<TNumber>.Zero, surface.Size.Width);
		var parametersY = (value: point.Y, Number<TNumber>.Zero, surface.Size.Height);

		InRangeNoOutputGuard<TNumber>.Guard(validator, parametersX, parametersX, errorCode, innerException);
		InRangeNoOutputGuard<TNumber>.Guard(validator, parametersY, parametersY, errorCode, innerException);
		
		return point;
	}
}