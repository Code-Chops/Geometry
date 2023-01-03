using CodeChops.Geometry.Space.Directions;

namespace CodeChops.Geometry.Space.Movements;

/// <summary>
/// Should be used for objects that don't move. It can still hold a direction.
/// </summary>
public record NoMovement<TNumber> : Movement<TNumber>, IHasDefault<NoMovement<TNumber>>
	where TNumber : INumber<TNumber>
{
	public static NoMovement<TNumber> Default { get; } = new(Point<TNumber>.Default);

	public sealed override IDirection<TNumber> GetDirection() => this._direction;
	private readonly IDirection<TNumber> _direction;
	
	protected sealed override Point<TNumber> CalculatePoint() => this.StartingPoint;

	public NoMovement(Point<TNumber> point, IDirection<TNumber>? direction = null)
		: base(startingPoint: point)
	{
		this._direction = direction ?? NoDirection<TNumber>.Instance;
	}
}