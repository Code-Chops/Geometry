namespace CodeChops.Geometry.Time.Epoch;

/// <summary>
/// Helper to convert DateTimes to Unix time stamps and vice versa.
/// </summary>
public static class UnixEpochConverter
{
	/// <summary>
	/// The Unix Epoch, which is January 1st 1970 at 00:00:00.000 UTC
	/// </summary>
	public static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

	/// <summary>
	/// Converts the given DateTime to the Unix timestamp in seconds.
	/// </summary>
	public static ulong ToUnixTimeSeconds(DateTime dateTime)
	{
		return (ulong)dateTime.Subtract(Epoch).TotalSeconds;
	}

	/// <summary>
	/// Converts the given DateTime to the Unix timestamp in milliseconds.
	/// </summary>
	public static ulong ToUnixTimeMilliseconds(DateTime dateTime)
	{
		return (ulong)dateTime.Subtract(Epoch).TotalMilliseconds;
	}

	/// <summary>
	/// Converts the given Unix timestamp in seconds to a DateTime.
	/// </summary>
	public static DateTime FromUnixTimeSeconds(ulong unixTimeSeconds)
	{
		return Epoch.AddSeconds(unixTimeSeconds);
	}

	/// <summary>
	/// Converts the given Unix timestamp in milliseconds to a DateTime.
	/// </summary>
	public static DateTime FromUnixTimeMilliseconds(ulong unixTimeMilliseconds)
	{
		return Epoch.AddMilliseconds(unixTimeMilliseconds);
	}
}