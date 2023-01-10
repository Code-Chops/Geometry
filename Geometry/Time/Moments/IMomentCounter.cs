namespace CodeChops.Geometry.Time.Moments;

/// <summary>
/// A moment counter helps calculating the direction and location of a moving object deterministically.
/// The moments can be calculated in moments over time using <see cref="TimedMomentCounter"/>, or manual moments using <see cref="ManualMomentCounter"/>.
/// </summary>
public interface IMomentCounter
{
	public TNumber GetMoments<TNumber>()
		where TNumber: INumber<TNumber>;

	public long Moments { get; }
	public bool IsRunning { get; }
	public void Start();
	public void Stop();
	public void Restart();	
}