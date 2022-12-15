namespace CodeChops.Geometry.Space;

public static class InRangeGuardExtensions
{
	/// <summary>
	/// Checks if the provided address is in the surface's bounds. 
	/// </summary>
	public static bool GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, Number<TNumber> value, 
		string? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		validator.GuardInRange<TNumber>(value, Number<TNumber>.Zero, surface.Area, errorCode, innerException);

		return validator.IsValid;
	}
	
	/// <summary>
	/// Checks if the provided point is in the surface's bounds. 
	/// </summary>
	public static Number<TNumber> GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, Point<TNumber> point, 
		string? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		var address = surface.Size.GetAddress(point, surface.Offset);
		validator.GuardInRange(surface, address, errorCode, innerException);
		
		return address;
	}
}