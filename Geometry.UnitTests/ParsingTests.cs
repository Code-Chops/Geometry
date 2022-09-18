using CodeChops.Geometry.Space;
using CodeChops.Geometry.Space.Points;

namespace CodeChops.Geometry.UnitTests;

public class ParsingTests
{
	[Theory]
	[InlineData(0,		0,		"left: 0px; top: 0px; ")]
	[InlineData(0.5,	0.5,	"left: 0.5px; top: 0.5px; ")]
	[InlineData(1.02,	1.02,	"left: 1.02px; top: 1.02px; ")]
	public void InlineCss_ShouldUse_InvariantCulture(double x, double y, string expectedInlineCss)
	{
		var inlineCss = new Point<double>(x, y).InlineCss();
		Assert.Equal(expectedInlineCss, inlineCss);
	}
}