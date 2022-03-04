using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CodeChops.Geometry.Time;

public static class StopwatchExtensions
{
	#region Configuration

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered stopwatch through the <see cref="StopwatchScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the stopwatch that is exposed by default.
	/// </para>
	/// </summary>
	public static IApplicationBuilder UseStopwatchScope(this IApplicationBuilder applicationBuilder, IStopwatch stopwatch)
	{
		UseStopwatchScope(applicationBuilder.ApplicationServices, stopwatch);
		return applicationBuilder;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered stopwatch through the <see cref="StopwatchScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the stopwatch that is exposed by default.
	/// </para>
	/// </summary>
	public static IHost UseStopwatchScope(this IHost host, IStopwatch stopwatch)
	{
		UseStopwatchScope(host.Services, stopwatch);
		return host;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered stopwatch through the <see cref="StopwatchScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the stopwatch that is exposed by default.
	/// </para>
	/// </summary>
	public static IServiceProvider UseStopwatchScope(this IServiceProvider serviceProvider, IStopwatch stopwatch)
	{
		StopwatchScope.SetDefaultValue(stopwatch ?? throw new ArgumentNullException(nameof(stopwatch)));
		return serviceProvider;
	}

	#endregion
}