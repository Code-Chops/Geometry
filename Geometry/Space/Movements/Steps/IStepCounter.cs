namespace CodeChops.Geometry.Space.Movements.Steps;

/// <summary>
/// A step counter helps calculating the direction and location of a moving object deterministically.
/// The steps can be calculated in steps over time using <see cref="TimedStepCounter"/>, or manual steps using <see cref="ManualStepCounter"/>.
/// </summary>
public interface IStepCounter
{
	public TNumber GetSteps<TNumber>()
		where TNumber: INumber<TNumber>;

	public long Steps { get; }
	public bool IsRunning { get; }
	public void Start();
	public void Stop();
	public void Restart();	
}