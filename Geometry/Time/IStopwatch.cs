namespace CodeChops.Geometry.Time;

public interface IStopwatch
{
    TimeSpan Elapsed { get; }
    long ElapsedMilliseconds { get; }
    void Reset();
}
