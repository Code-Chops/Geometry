namespace CodeChops.Geometry.Space.Surface;

public interface ISurface<TNumber> : ISurface
	where TNumber : INumber<TNumber>
{
	Size<TNumber> Size { get; }
	Point<TNumber> Offset { get; }
	TNumber Circumference { get; }
	TNumber Area { get; }
	
	Surface<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : struct, INumber<TTargetNumber>, IPowerFunctions<TTargetNumber>, IRootFunctions<TTargetNumber>;
}

public interface ISurface : IDomainObject
{
}