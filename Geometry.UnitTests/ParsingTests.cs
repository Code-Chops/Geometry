using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;
using CodeChops.GenericMath.Serialization.Json;
using CodeChops.Geometry.Space.Points;
using CodeChops.MagicEnums.Json;

namespace CodeChops.Geometry.UnitTests;

public class ParsingTests
{
	static ParsingTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new MagicEnumJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new IdentityJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());
	}
	
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