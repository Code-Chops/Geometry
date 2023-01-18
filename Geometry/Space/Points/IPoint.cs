namespace CodeChops.Geometry.Space.Points;

public interface IPoint<out TNumber> : IPoint
	where TNumber : INumber<TNumber>
{
	TNumber X { get; }
	TNumber Y { get; }

	/// <summary>
	/// Sums up X and Y.
	/// </summary>
	TNumber Sum();

	/// <summary>
	/// Multiplies X and Y.
	/// </summary>
	TNumber Multiply();
}

public interface IPoint : IValueObject
{
	Point<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>;
}