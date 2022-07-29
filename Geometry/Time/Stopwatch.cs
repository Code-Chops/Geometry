using CodeChops.DomainDrivenDesign.DomainModeling;
using CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

namespace CodeChops.Geometry.Time;

[GenerateEntityId]
public partial class Stopwatch : Entity, IStopwatch
{
    private System.Diagnostics.Stopwatch Instance { get; }

    public bool IsRunning => this.Instance.IsRunning;
    public TimeSpan Elapsed => this.Instance.Elapsed;
    public long ElapsedMilliseconds => this.Instance.ElapsedMilliseconds;

    public void Start() => this.Instance.Start();
    public void Stop() => this.Instance.Stop();
    public void Reset() => this.Instance.Reset();
    public void Restart() => this.Instance.Restart();

    public Stopwatch(System.Diagnostics.Stopwatch? instance = null)
    {
        this.Instance = instance ?? new System.Diagnostics.Stopwatch();
    }
}