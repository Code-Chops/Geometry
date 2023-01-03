using System.Diagnostics;
using CodeChops.Geometry.Time;

namespace CodeChops.Geometry.Space.Movements.Steps;

/// <summary>
/// A counter in which the steps are incremented over time, using a stopwatch.
/// </summary>
[GenerateIdentity]
public sealed partial class TimedStepCounter : Entity, IStepCounter, IStopwatch
{
    public override string ToString() => this.ToDisplayString();
    private Stopwatch StopWatch { get; }

    public long Steps => this.StepsGetter(this.StopWatch);

    public TNumber GetSteps<TNumber>() 
	    where TNumber : INumber<TNumber> 
	    => TNumber.CreateChecked(this.StepsGetter(this.StopWatch));
    
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