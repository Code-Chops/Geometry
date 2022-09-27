﻿namespace CodeChops.Geometry.Time;

[GenerateStronglyTypedId]
public sealed partial class Stopwatch : Entity, IStopwatch
{
    public override string ToString() => $"{this.GetType().Name} {{ {nameof(this.ElapsedMilliseconds)} = {this.ElapsedMilliseconds} }}";
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