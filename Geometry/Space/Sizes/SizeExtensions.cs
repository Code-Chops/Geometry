namespace CodeChops.Geometry.Space.Sizes;

public static class SizeExtensions
{
	public static Dictionary<string, object> InlineCss<TNumber>(this Size<TNumber> size)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		return new()
		{
			["width"] = size.Width,
			["height"] = size.Height
		};
	}
}