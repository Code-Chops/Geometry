using System.Diagnostics;

namespace CodeChops.Geometry.Time;

/// <summary>
/// A timer for which the speed can be changed using <see cref="ChangeSpeed(double)"/>. The speed can also be manually increased by using <see cref="AddTime"/>.
/// </summary>
[GenerateIdentity]
public partial class CustomSpeedTimer : Entity<CustomSpeedTimer.Identity>, ITimer
{
	public override string ToString() => this.ToDisplayString();

	/// <summary>
	/// When increasing or decreasing the speed, the current time will be saved here and <see cref="_currentTime"/> will be reset.
	/// </summary>
	private TimeSpan _elapsedTime;
	
	/// <summary>
	/// This time is used (in combination with <see cref="SpeedFactor"/> to calculate the new time and will be added to <see cref="_elapsedTime"/>. 
	/// </summary>
	private Stopwatch _currentTime;
	
	/// <summary>
	/// The factor of the speed.
	/// </summary>
	public double SpeedFactor { get; private set; }
	
	public TimeSpan Elapsed => this._elapsedTime + this._currentTime.Elapsed * this.SpeedFactor;
	public long ElapsedMilliseconds => (long)(this._elapsedTime.Milliseconds + this._currentTime.ElapsedMilliseconds * this.SpeedFactor);
	public long ElapsedTicks => (long)(this._elapsedTime.Ticks + this._currentTime.ElapsedTicks * this.SpeedFactor);
	public bool IsRunning => this._currentTime.IsRunning;

	public CustomSpeedTimer(Identity id, TimeSpan elapsedTime = default) 
		: base(id)
	{
		this._elapsedTime = elapsedTime;
		this._currentTime = new Stopwatch();
	}
	
	/// <inheritdoc cref="Stopwatch.Start()"/>
	/// <exception cref="InvalidOperationException">When already running.</exception>
	public void Start()
	{
		if (this.IsRunning)
			throw new InvalidOperationException($"{this.GetType().Name} is already running.");
		
		this._currentTime.Start();
	}

	/// <inheritdoc cref="Stopwatch.Stop()"/>
	/// <exception cref="InvalidOperationException">When already stopped.</exception>
	public void Stop()
	{
		if (!this.IsRunning)
			throw new InvalidOperationException($"{this.GetType().Name} already stopped running.");
		
		this._currentTime.Stop();
	}

	/// <inheritdoc cref="Stopwatch.Reset()"/>
	public void Reset()
	{
		this._elapsedTime = new TimeSpan();
		this._currentTime.Reset();
	}

	/// <inheritdoc cref="Stopwatch.Restart()"/>
	public void Restart()
	{
		this._elapsedTime = new TimeSpan();
		this._currentTime.Restart();
	}

	/// <summary>
	/// Changes the speed of the stopwatch.
	/// </summary>
	/// <param name="factor">The new speed as factor, so 1 = default.</param>
	public void ChangeSpeed(double factor)
	{
		if (Math.Abs(factor - this.SpeedFactor) < 0.001)
			return;
		
		this._currentTime.Stop();
		
		this._elapsedTime = this._currentTime.Elapsed;
		
		this._currentTime = new Stopwatch();
		this.SpeedFactor = factor;
		this._currentTime.Start();
	}

	public void AddTime(TimeSpan timeSpan)
	{
		this._elapsedTime += timeSpan;
	}
}