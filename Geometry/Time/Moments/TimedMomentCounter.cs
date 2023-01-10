using System.Diagnostics;

namespace CodeChops.Geometry.Time.Moments;

/// <summary>
/// A counter in which the moments are incremented over time, using a stopwatch.
/// </summary>
[GenerateIdentity]
public sealed partial class TimedMomentCounter : Entity, IMomentCounter, IStopwatch
{
    public override string ToString() => this.ToDisplayString();
    private Stopwatch StopWatch { get; }

    public long Moments => this.momentsCountGetter(this.StopWatch);

    public TNumber GetMoments<TNumber>() 
	    where TNumber : INumber<TNumber> 
	    => TNumber.CreateChecked(this.momentsCountGetter(this.StopWatch));
    
    public bool IsRunning => this.StopWatch.IsRunning;
    public TimeSpan Elapsed => this.StopWatch.Elapsed;
    public long ElapsedMilliseconds => this.StopWatch.ElapsedMilliseconds;
    public long ElapsedTicks => this.StopWatch.ElapsedTicks;
    
    public void Start() => this.StopWatch.Start();
    public void Stop() => this.StopWatch.Stop();
    public void Restart() => this.StopWatch.Restart();
    public void Reset() => this.StopWatch.Reset();

    private Func<Stopwatch, long> momentsCountGetter { get; }
    
    public TimedMomentCounter(Stopwatch? instance = null, Func<Stopwatch, long>? momentsCountGetter = null)
    {
	    this.StopWatch = instance ?? new Stopwatch();
	    this.momentsCountGetter = momentsCountGetter ?? (static stopwatch => stopwatch.ElapsedMilliseconds);
    }
}