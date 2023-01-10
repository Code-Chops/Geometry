using Architect.AmbientContexts;

namespace CodeChops.Geometry.Time.Moments;

/// <summary>
/// <para>
/// Controls access to a moment counter in the ambient context. This way a statically shared moment counter can be accessed and controlled from the outside.
/// </para>
/// <para>
/// This functionality makes use of Ambient Context pattern as implemented by TheArchitectDev. See https://github.com/TheArchitectDev/Architect.AmbientContexts.
/// </para>
/// </summary>
public sealed class MomentCounterScope : AmbientScope<MomentCounterScope>
{
	public override string ToString() => this.GetType().Name;
	
	static MomentCounterScope()
	{
		SetDefaultValue(new TimedMomentCounter());
	}

	/// <summary>
	/// Returns the currently accessible <see cref="MomentCounterScope"/>.
	/// The scope is configurable from the outside, such as from startup.
	/// </summary>
	public static MomentCounterScope Current => GetAmbientScope() ?? throw new InvalidOperationException(
		$"{nameof(MomentCounterScope)} was not configured. Call {nameof(ClockScopeExtensions)}.{nameof(ClockScopeExtensions.UseClockScope)} on startup.");

	public IMomentCounter Value { get; }

	/// <summary>
	/// Establishes the given moment counter as the ambient one until the scope is disposed.
	/// </summary>
	public MomentCounterScope(IMomentCounter momentCounter)
		: this(momentCounter, AmbientScopeOption.ForceCreateNew)
	{
		this.Activate();
	}

	/// <summary>
	/// Private constructor.
	/// Does not activate this instance.
	/// </summary>
	private MomentCounterScope(IMomentCounter momentCounter, AmbientScopeOption ambientScopeOption)
		: base(ambientScopeOption)
	{
		this.Value = momentCounter ?? throw new ArgumentNullException(nameof(momentCounter));
	}

	protected override void DisposeImplementation()
	{
		// Nothing to dispose
	}

	/// <summary>
	/// Sets the ubiquitous default scope, overwriting and disposing the previous one if necessary.
	/// </summary>
	internal static void SetDefaultValue(IMomentCounter momentCounter)
	{
		SetDefaultScope(new MomentCounterScope(momentCounter, AmbientScopeOption.NoNesting));
	}
}