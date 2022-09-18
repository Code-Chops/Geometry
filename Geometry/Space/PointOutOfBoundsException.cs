using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;
using CodeChops.DomainDrivenDesign.DomainModeling.TypeExtensions;
using CodeChops.GenericMath.Serialization;
using CodeChops.Geometry.Space.Points;

namespace CodeChops.Geometry.Space;

public class PointOutOfBoundsException<TSurface, TPoint>
	: SystemException<PointOutOfBoundsException<TSurface, TPoint>, (TSurface, TPoint)>, ISystemException<PointOutOfBoundsException<TSurface, TPoint>, (TSurface, TPoint)>
	where TSurface : ISurface
	where TPoint : IPoint
{
	private static readonly JsonSerializerOptions SerializerOptions = new() { Converters = { new ValueTupleFactory(), new NumberJsonConverterFactory() }};
	
	public static string ErrorMessage => $"Trying to get a point from {typeof(TSurface).GetSimpleName()} that is out of bounds";
	
	public static PointOutOfBoundsException<TSurface, TPoint> Create((TSurface, TPoint) parameter) 
		=> new(parameter);
	private PointOutOfBoundsException((TSurface, TPoint) parameters) : base(parameters, jsonSerializerOptions: SerializerOptions) { }
}