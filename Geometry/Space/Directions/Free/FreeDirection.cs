namespace CodeChops.Geometry.Space.Directions.Free;

/// <summary>
/// A direction in every possible delta point. Can also be defined by an angle.
/// </summary>
[GenerateValueObject(underlyingType: typeof(Point<>), generateToString: false, propertyIsPublic: true, generateDefaultConstructor: false, forbidParameterlessConstruction: true)]
public readonly partial record struct FreeDirection<TNumber> : IDirection<TNumber>
	where TNumber : INumber<TNumber>
{
	public override partial string ToString() => $"{nameof(FreeDirection<TNumber>)}: {this.Value}";

	public Angle Angle { get; }
	
	// ReSharper disable once UnusedParameter.Local
	public FreeDirection(Point<TNumber> value, Validator? _ = null)
	{	
		this._value = value;
		this.Angle = this.Value.ToAngle();
	}
	
	public FreeDirection(Angle angle)
	{
		this._value = new Point<TNumber>(angle);
		this.Angle = angle;
	}

	private static Random Random { get; } = new();
	public static FreeDirection<TNumber> GetNewDirectionFromRandomTurn(Random? random = null)
	{
		return new(new Angle((random ?? Random).NextDouble() * 360));
	}

	public FreeDirection<TNumber> GetNewDirectionFromTurn(Angle angleDelta)
	{
		return new(new Angle(this.Angle + angleDelta));
	}
	
	/// <summary>
	/// Converts the free direction into a free direction of a different <typeparamref name="TTargetNumber"/>.
	/// </summary>
	/// <typeparam name="TTargetNumber">The number type to be converted to.</typeparam>
	public FreeDirection<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>
	{
		return new(this.Value.Convert<TTargetNumber>());
	}
}