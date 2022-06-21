using CodeChops.Geometry.Space;
using CodeChops.MagicEnums.Attributes;
using CodeChops.MagicEnums;
using Xunit;

namespace CodeChops.Geometry.UnitTests;

public class ParsingTests
{
	[Theory]
	[InlineData(0,		0,		"left: 0.00px; top: 0.00px; ")]
	[InlineData(0.5,	0.5,	"left: 0.50px; top: 0.50px; ")]
	[InlineData(1.02,	1.02,	"left: 1.02px; top: 1.02px; ")]
	public void InlineCss_ShouldUse_InvariantCulture(double x, double y, string expectedInlineCss)
	{
		var inlineCss = new Point<double>(x, y).InlineCss();
		Assert.Equal(expectedInlineCss, inlineCss);
	}
}