namespace CodeChops.Geometry.Time;

public interface ITimer
{
	TimeSpan Elapsed { get; }
	long ElapsedMilliseconds { get; }
	long ElapsedTicks { get; }
	bool IsRunning { get; }
	void Start();
	void Stop();
	void Reset();
	void Restart();	
}