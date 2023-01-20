namespace CodeChops.Geometry.Space.Movements;

public interface IDynamicMovement<TNumber> : IMovement<TNumber>
	where TNumber : INumber<TNumber>
{
}