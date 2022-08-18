namespace CodeChops.Geometry.Time;

public interface IStopwatch
{
    string ToString();
    bool IsRunning { get; }
    TimeSpan Elapsed { get; }
    long ElapsedMilliseconds { get; }

    void Start();
    void Stop();
    void Restart();
    void Reset();
}
