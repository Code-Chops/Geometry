namespace CodeChops.Geometry.Space.Lines;

public interface ILine<TNumber> : ILine, IComparable<Line<TNumber>>
	where TNumber : INumber<TNumber>
{
	Point<TNumber> StartingPoint { get; }
	Point<TNumber> Endpoint { get; }

	Line<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>, IPowerFunctions<TTargetNumber>, IRootFunctions<TTargetNumber>;
}

public interface ILine : IValueObject
{
}