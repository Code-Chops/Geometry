using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.GenericMath.Serialization.Json;
using CodeChops.Geometry.Space.Directions.Free;
using CodeChops.Geometry.Space.Points;
using CodeChops.MagicEnums.Json;

namespace CodeChops.Geometry.UnitTests;

public class FreeDirectionTests
{
	static FreeDirectionTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new MagicEnumJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new IdentityJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());
	}
	
	[Theory]
	[InlineData(0,		 0, -1)]
	[InlineData(90,		 1,  0)]
	[InlineData(180,	 0,  1)]
	[InlineData(-90,	-1,  0)]
	public void AngleToDeltaPoint_Is_Correct(double angle, int expectedDeltaX, int expectedDeltaY)
	{
		var direction = new FreeDirection<float>(angle);
		var (x, y) = direction.GetValue<float>();
		var expectedPoint = new Point<float>(expectedDeltaX, expectedDeltaY);

		Assert.Equal((int)expectedPoint.X, (int)x);
		Assert.Equal((int)expectedPoint.Y, (int)y);

		direction = new FreeDirection<float>(expectedPoint);

		Assert.Equal(angle, direction.Angle);
	}
}