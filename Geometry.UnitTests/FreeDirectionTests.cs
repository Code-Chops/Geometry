using CodeChops.DomainModeling.Serialization.Json;
using CodeChops.Geometry.Space.Directions.Free;
using CodeChops.Geometry.Space.Points;
using CodeChops.MagicEnums.Json;

namespace CodeChops.Geometry.UnitTests;

public class FreeDirectionTests
{
	static FreeDirectionTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new MagicEnumJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new ValueObjectJsonConverterFactory());
	}

	[Theory]
	[InlineData(0,		 0, -1)]
	[InlineData(90,		 1,  0)]
	[InlineData(180,	 0,  1)]
	[InlineData(-90,	-1,  0)]
	public void AngleToDeltaPoint_Is_Correct(double degrees, int expectedDeltaX, int expectedDeltaY)
	{
		var angle = new Angle(degrees);
		var direction = new FreeDirection<double>(angle);
		var (x, y) = direction.Value;
		var expectedPoint = new Point<double>(expectedDeltaX, expectedDeltaY);

		Assert.Equal((int)expectedPoint.X, (int)x);
		Assert.Equal((int)expectedPoint.Y, (int)y);

		direction = new FreeDirection<double>(expectedPoint);

		Assert.Equal(angle, direction.Angle);
	}
}