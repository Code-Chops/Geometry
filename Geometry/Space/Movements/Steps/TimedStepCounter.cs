using System.Diagnostics;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements.Steps;

[GenerateIdentity]
public sealed partial class TimedStepCounter : Entity, IStepCounter, IStopwatch
{
    public override string ToString() => this.ToDisplayString();
    private Stopwatch StopWatch { get; }

    public long Steps => this.StepsGetter(this.StopWatch);
    
    public bool IsRunning => this.StopWatch.IsRunning;
    public TimeSpan Elapsed => this.StopWatch.Elapsed;
    public long ElapsedMilliseconds => this.StopWatch.ElapsedMilliseconds;
    public long ElapsedTicks => this.StopWatch.ElapsedTicks;
    
    public void Start() => this.StopWatch.Start();
    public void Stop() => this.StopWatch.Stop();
    public void Restart() => this.StopWatch.Restart();
    public void Reset() => this.StopWatch.Reset();

    private Func<Stopwatch, long> StepsGetter { get; }
    
    public TimedStepCounter(Stopwatch? instance = null, Func<Stopwatch, long>? stepsGetter = null)
    {
	    this.StopWatch = instance ?? new Stopwatch();
	    this.StepsGetter = stepsGetter ?? (static stopwatch => stopwatch.ElapsedMilliseconds);
    }
}