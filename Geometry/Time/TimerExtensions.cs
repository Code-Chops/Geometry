using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CodeChops.Geometry.Time;

public static class TimerExtensions
{
	#region Configuration

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered timer through the <see cref="TimerScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the timer that is exposed by default.
	/// </para>
	/// </summary>
	public static IApplicationBuilder UseTimerScope(this IApplicationBuilder applicationBuilder, ITimer timer)
	{
		UseTimerScope(applicationBuilder.ApplicationServices, timer);
		return applicationBuilder;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered timer through the <see cref="TimerScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the timer that is exposed by default.
	/// </para>
	/// </summary>
	public static IHost UseTimerScope(this IHost host, ITimer timer)
	{
		UseTimerScope(host.Services, timer);
		return host;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered timer through the <see cref="TimerScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the timer that is exposed by default.
	/// </para>
	/// </summary>
	public static IServiceProvider UseTimerScope(this IServiceProvider serviceProvider, ITimer timer)
	{
		TimerScope.SetDefaultValue(timer ?? throw new ArgumentNullException(nameof(timer)));
		return serviceProvider;
	}

	#endregion
}