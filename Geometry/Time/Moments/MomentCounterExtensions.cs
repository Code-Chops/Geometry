using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CodeChops.Geometry.Time.Moments;

public static class MomentCounterExtensions
{
	#region Configuration

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered moment counter through the <see cref="MomentCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the moment counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IApplicationBuilder UseMomentCounterScope(this IApplicationBuilder applicationBuilder, IMomentCounter momentCounter)
	{
		UseMomentCounterScope(applicationBuilder.ApplicationServices, momentCounter);
		return applicationBuilder;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered moment counter through the <see cref="MomentCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the moment counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IHost UseMomentCounterScope(this IHost host, IMomentCounter momentCounter)
	{
		UseMomentCounterScope(host.Services, momentCounter);
		return host;
	}

	/// <summary>
	/// <para>
	/// Enables static, injection-free access to the registered moment counter through the <see cref="MomentCounterScope"/> class.
	/// </para>
	/// <para>
	/// This overwrites the moment counter that is exposed by default.
	/// </para>
	/// </summary>
	public static IServiceProvider UseMomentCounterScope(this IServiceProvider serviceProvider, IMomentCounter momentCounter)
	{
		MomentCounterScope.SetDefaultValue(momentCounter ?? throw new ArgumentNullException(nameof(momentCounter)));
		return serviceProvider;
	}

	#endregion
}