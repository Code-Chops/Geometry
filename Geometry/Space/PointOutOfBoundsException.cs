using CodeChops.Geometry.Space.Points;
using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

namespace CodeChops.Geometry.Space;

public sealed record PointOutOfBoundsException<TSurface, TPoint>(string? CustomMessage = null) 
	: SystemException<PointOutOfBoundsException<TSurface, TPoint>, TPoint>(CustomMessage ?? $"Trying to get a point from {typeof(TSurface).GetSimpleName()} that is out of bounds")
	where TSurface : ISurface
	where TPoint : IPoint;