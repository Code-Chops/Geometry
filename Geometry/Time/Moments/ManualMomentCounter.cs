namespace CodeChops.Geometry.Time.Moments;

/// <summary>
/// A counter in which the moments are incremented manually until maximum moments.
/// </summary>
[GenerateIdentity]
public partial class ManualMomentCounter : Entity, IMomentCounter
{
	public long Moments { get; private set; }
	
	public TNumber GetMoments<TNumber>()
		where TNumber : INumber<TNumber>
		=> TNumber.CreateChecked(this.Moments);
	
	public bool IsRunning { get; private set; }
	public long MaximumMoments { get; }

	public ManualMomentCounter(long maximumMoments)
	{
		this.MaximumMoments = maximumMoments;
	}
	
	public void Start()
	{
		if (this.IsRunning)
			throw new InvalidOperationException($"{this.GetType().Name} is already running.");
		
		this.IsRunning = true;
	}

	public void Stop()
	{
		if (!this.IsRunning)
			throw new InvalidOperationException($"{this.GetType().Name} already stopped running.");
		
		this.IsRunning = false;
	}

	public void Restart()
	{
		this.Moments = 0;
		
		if (!this.IsRunning)
			this.Start();
	}

	public void NextMoment()
	{
		if (!this.IsRunning)
			return;
		
		this.Moments++;
		
		if (this.Moments >= this.MaximumMoments) 
			this.Stop();
	}
}