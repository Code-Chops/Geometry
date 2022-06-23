using Architect.AmbientContexts;

namespace CodeChops.Geometry.Time;

/// <summary>
/// Controls access to a stopwatch in the ambient context.
/// </summary>
public class StopwatchScope : AmbientScope<StopwatchScope>
{
	static StopwatchScope()
	{
		SetDefaultValue(new Stopwatch());
	}

	/// <summary>
	/// Returns the currently accessible <see cref="StopwatchScope"/>.
	/// The scope is configurable from the outside, such as from startup.
	/// </summary>
	public static StopwatchScope Current => GetAmbientScope() ?? throw new InvalidOperationException(
		$"{nameof(StopwatchScope)} was not configured. Call {nameof(ClockScopeExtensions)}.{nameof(ClockScopeExtensions.UseClockScope)} on startup.");

	public IStopwatch Value { get; }

	/// <summary>
	/// Establishes the given stopwatch as the ambient one until the scope is disposed.
	/// </summary>
	public StopwatchScope(IStopwatch stopwatch)
		: this(stopwatch, AmbientScopeOption.ForceCreateNew)
	{
		this.Activate();
	}

	/// <summary>
	/// Private constructor.
	/// Does not activate this instance.
	/// </summary>
	private StopwatchScope(IStopwatch stopwatch!!, AmbientScopeOption ambientScopeOption)
		: base(ambientScopeOption)
	{
		this.Value = stopwatch;
	}

	protected override void DisposeImplementation()
	{
		// Nothing to dispose
	}

	/// <summary>
	/// Sets the ubiquitous default scope, overwriting and disposing the previous one if necessary.
	/// </summary>
	internal static void SetDefaultValue(IStopwatch stopwatch)
	{
		var newDefaultScope = stopwatch is null
			? null
			: new StopwatchScope(stopwatch, AmbientScopeOption.NoNesting);
		SetDefaultScope(newDefaultScope);
	}
}