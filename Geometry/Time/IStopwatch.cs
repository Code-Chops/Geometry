namespace CodeChops.Geometry.Time;

public interface IStopwatch
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
