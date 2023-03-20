using Architect.AmbientContexts;

namespace CodeChops.Geometry.Time;

/// <summary>
/// <para>
/// Controls access to a timer in the ambient context. This way a statically shared timer can be accessed and controlled from the outside.
/// </para>
/// <para>
/// This functionality makes use of Ambient Context pattern as implemented by TheArchitectDev. See https://github.com/TheArchitectDev/Architect.AmbientContexts.
/// </para>
/// </summary>
public sealed class TimerScope : AmbientScope<TimerScope>
{
	public override string ToString() => this.GetType().Name;
	
	static TimerScope()
	{
		SetDefaultValue(new CustomSpeedTimer() { Id = default});
	}

	/// <summary>
	/// Returns the currently accessible <see cref="TimerScope"/>.
	/// The scope is configurable from the outside, such as from startup.
	/// </summary>
	public static TimerScope Current => GetAmbientScope() ?? throw new InvalidOperationException(
		$"{nameof(TimerScope)} was not configured. Call {nameof(ClockScopeExtensions)}.{nameof(ClockScopeExtensions.UseClockScope)} on startup.");

	public ITimer Value { get; }

	/// <summary>
	/// Establishes the given timer as the ambient one until the scope is disposed.
	/// </summary>
	public TimerScope(ITimer timer)
		: this(timer, AmbientScopeOption.ForceCreateNew)
	{
		this.Activate();
	}

	/// <summary>
	/// Private constructor.
	/// Does not activate this instance.
	/// </summary>
	private TimerScope(ITimer timer, AmbientScopeOption ambientScopeOption)
		: base(ambientScopeOption)
	{
		this.Value = timer ?? throw new ArgumentNullException(nameof(timer));
	}

	protected override void DisposeImplementation()
	{
		// Nothing to dispose
	}

	/// <summary>
	/// Sets the ubiquitous default scope, overwriting and disposing the previous one if necessary.
	/// </summary>
	internal static void SetDefaultValue(ITimer timer)
	{
		SetDefaultScope(new TimerScope(timer, AmbientScopeOption.NoNesting));
	}
}