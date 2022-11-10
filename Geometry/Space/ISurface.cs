namespace CodeChops.Geometry.Space;

public interface ISurface<TNumber> : ISurface
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	Size<TNumber> Size { get; }
	Point<TNumber> Offset { get; }
	Number<TNumber> Circumference { get; }
	Number<TNumber> Area { get; }
}

public interface ISurface : IDomainObject
{
}