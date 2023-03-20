namespace CodeChops.Geometry.Time;

[GenerateValueObject<long>(minimumValue: 0, useCustomProperty: true)]
public readonly partial record struct ElapsedMilliseconds
{
	private long Value => this._value ?? TimerScope.Current.Value.ElapsedMilliseconds;
	private readonly long? _value;
	
	public ElapsedMilliseconds(long? value = null, Validator? validator = null)
	{	
		validator ??= Validator.Get<ElapsedMilliseconds>.Default;
		
		if (value is not null)
			validator.GuardInRange(value.Value, minimum: 0, maximum: null, errorCode: null);
		
		this._value = value;
	}
}
