namespace CodeChops.Geometry.Space.Sizes;

public static class SizeExtensions
{
	public static Dictionary<string, object> InlineCss<TNumber>(this Size<TNumber> size)
		where TNumber : INumber<TNumber>
	{
		return new()
		{
			["width"] = size.Width,
			["height"] = size.Height
		};
	}
}