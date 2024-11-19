using System.Diagnostics;

namespace CodeChops.Geometry.Time;

/// <summary>
/// A timer which wraps a <see cref="System.Diagnostics.Stopwatch"/>
/// </summary>
[GenerateIdentity]
public sealed class Timer(Stopwatch? instance = null) : ITimer
{
    public override string ToString() => this.GetType().Name;

    private Stopwatch Stopwatch { get; } = instance ?? new Stopwatch();

    public bool IsRunning => this.Stopwatch.IsRunning;
    public TimeSpan Elapsed => this.Stopwatch.Elapsed;
    public long ElapsedMilliseconds => this.Stopwatch.ElapsedMilliseconds;
    public long ElapsedTicks => this.Stopwatch.ElapsedTicks;

    public void Start() => this.Stopwatch.Start();
    public void Stop() => this.Stopwatch.Stop();
    public void Restart() => this.Stopwatch.Restart();
    public void Reset() => this.Stopwatch.Reset();
}