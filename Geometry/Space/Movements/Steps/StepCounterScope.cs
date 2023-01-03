using Architect.AmbientContexts;

namespace CodeChops.Geometry.Space.Movements.Steps;

/// <summary>
/// <para>
/// Controls access to a step counter in the ambient context. This way a statically shared step counter can be accessed and controlled from the outside.
/// </para>
/// <para>
/// This functionality makes use of Ambient Context pattern as implemented by TheArchitectDev. See https://github.com/TheArchitectDev/Architect.AmbientContexts.
/// </para>
/// </summary>
public sealed class StepCounterScope : AmbientScope<StepCounterScope>
{
	public override string ToString() => this.GetType().Name;
	
	static StepCounterScope()
	{
		SetDefaultValue(new TimedStepCounter());
	}

	/// <summary>
	/// Returns the currently accessible <see cref="StepCounterScope"/>.
	/// The scope is configurable from the outside, such as from startup.
	/// </summary>
	public static StepCounterScope Current => GetAmbientScope() ?? throw new InvalidOperationException(
		$"{nameof(StepCounterScope)} was not configured. Call {nameof(ClockScopeExtensions)}.{nameof(ClockScopeExtensions.UseClockScope)} on startup.");

	public IStepCounter Value { get; }

	/// <summary>
	/// Establishes the given step counter as the ambient one until the scope is disposed.
	/// </summary>
	public StepCounterScope(IStepCounter stepCounter)
		: this(stepCounter, AmbientScopeOption.ForceCreateNew)
	{
		this.Activate();
	}

	/// <summary>
	/// Private constructor.
	/// Does not activate this instance.
	/// </summary>
	private StepCounterScope(IStepCounter stepCounter, AmbientScopeOption ambientScopeOption)
		: base(ambientScopeOption)
	{
		this.Value = stepCounter ?? throw new ArgumentNullException(nameof(stepCounter));
	}

	protected override void DisposeImplementation()
	{
		// Nothing to dispose
	}

	/// <summary>
	/// Sets the ubiquitous default scope, overwriting and disposing the previous one if necessary.
	/// </summary>
	internal static void SetDefaultValue(IStepCounter stepCounter)
	{
		SetDefaultScope(new StepCounterScope(stepCounter, AmbientScopeOption.NoNesting));
	}
}