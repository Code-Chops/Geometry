namespace CodeChops.Geometry.Time;

public interface IStopwatch
{
    bool IsRunning { get; }
    TimeSpan Elapsed { get; }
    long ElapsedMilliseconds { get; }

    void Start();
    void Stop();
    void Restart();
    void Reset();
}
