namespace CodeChops.Geometry.Space.Surface;

public static class InRangeGuardExtensions
{
	/// <summary>
	/// Checks if the provided address is in the surface's bounds. 
	/// </summary>
	public static bool GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, TNumber value, 
		string? errorCode, Exception? innerException = null)
		where TNumber : struct, INumber<TNumber>
	{
		validator.GuardInRange(value, TNumber.Zero, surface.Area, errorCode, innerException);

		return validator.IsValid;
	}
	
	/// <summary>
	/// Checks if the provided point is in the surface's bounds. 
	/// </summary>
	/// <returns>The address of the point on the surface.</returns>
	public static TNumber GuardInRange<TNumber>(this Validator validator, ISurface<TNumber> surface, Point<TNumber> point, 
		string? errorCode, Exception? innerException = null)
		where TNumber : struct, INumber<TNumber>
	{
		var address = surface.Size.GetAddress(point, surface.Offset);
		validator.GuardInRange(surface, address, errorCode, innerException);
		
		return address;
	}
}