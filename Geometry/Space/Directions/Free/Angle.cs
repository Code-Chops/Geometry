namespace CodeChops.Geometry.Space.Directions.Free;

/// <summary>
/// An angle (from 0 to 360 degrees). Modulates with 360.
/// </summary>
[GenerateValueObject<double>(minimumValue: -360, maximumValue: 360, useValidationExceptions: false, generateDefaultConstructor: false)]
public readonly partial record struct Angle
{
	// ReSharper disable once UnusedParameter.Local
	public Angle(double value, Validator? _ = null)
	{
		this._value = value % 360;
	}
}