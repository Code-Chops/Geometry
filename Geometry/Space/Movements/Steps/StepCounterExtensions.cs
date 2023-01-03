using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CodeChops.Geometry.Space.Movements.Steps;

public static class StepCounterExtensions
{
	#region Configuration

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered step counter through the <see cref="StepCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the step counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IApplicationBuilder UseStepCounterScope(this IApplicationBuilder applicationBuilder, IStepCounter stepCounter)
	{
		UseStepCounterScope(applicationBuilder.ApplicationServices, stepCounter);
		return applicationBuilder;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered step counter through the <see cref="StepCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the step counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IHost UseStepCounterScope(this IHost host, IStepCounter stepCounter)
	{
		UseStepCounterScope(host.Services, stepCounter);
		return host;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered step counter through the <see cref="StepCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the step counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IServiceProvider UseStepCounterScope(this IServiceProvider serviceProvider, IStepCounter stepCounter)
	{
		StepCounterScope.SetDefaultValue(stepCounter ?? throw new ArgumentNullException(nameof(stepCounter)));
		return serviceProvider;
	}

	#endregion
}