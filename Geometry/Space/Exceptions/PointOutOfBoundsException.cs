namespace CodeChops.Geometry.Space.Exceptions;

public class PointOutOfBoundsException<TSurface, TPoint, TSize> 
	: SystemException<PointOutOfBoundsException<TSurface, TPoint, TSize>, (TPoint, TSize)>, ISystemException<PointOutOfBoundsException<TSurface, TPoint, TSize>, (TPoint, TSize)>
{
	public static string ErrorMessage => $"Trying to get a location from {typeof(TSurface).Name} that is out of bounds.";
	
	public static PointOutOfBoundsException<TSurface, TPoint, TSize> Create((TPoint, TSize) parameter) 
		=> new(parameter);
	private PointOutOfBoundsException((TPoint, TSize) parameters) : base(parameters) { }
}