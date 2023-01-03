namespace CodeChops.Geometry.Space.Movements.Steps;

/// <summary>
/// A counter in which the steps are incremented manually until maximum steps.
/// </summary>
[GenerateIdentity]
public partial class ManualStepCounter : Entity, IStepCounter
{
	public long Steps { get; private set; }
	
	public TNumber GetSteps<TNumber>()
		where TNumber : INumber<TNumber>
		=> TNumber.CreateChecked(this.Steps);
	
	public bool IsRunning { get; private set; }
	public long MaximumSteps { get; }

	public ManualStepCounter(long maximumSteps)
	{
		this.MaximumSteps = maximumSteps;
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
		this.Steps = 0;
		
		if (!this.IsRunning)
			this.Start();
	}

	public void NextStep()
	{
		if (this.Steps + 1 >= this.MaximumSteps) 
			this.Stop();
		
		if (this.IsRunning)
			this.Steps++;
	}
}