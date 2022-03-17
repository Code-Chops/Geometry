using CodeChops.Geometry.Space;
using Xunit;

namespace CodeChops.Geometry.UnitTests;

public class FreeDirectionTests
{
	[Theory]
	[InlineData(0,		 0, -1)]
	[InlineData(90,		 1,  0)]
	[InlineData(180,	 0,  1)]
	[InlineData(-90,	-1,  0)]
	public void AngleToDeltaPoint_Is_Correct(double angle, int expectedDeltaX, int expectedDeltaY)
	{
		var direction = new FreeDirection(angle);
		var point = direction.GetValue().Cast<float, int>();
		var expectedPoint = new Point(expectedDeltaX, expectedDeltaY);

		Assert.Equal((int)expectedPoint.X, (int)point.X);
		Assert.Equal((int)expectedPoint.Y, (int)point.Y);

		direction.SetDeltaPoint(expectedPoint);

		Assert.Equal(angle, direction.Angle);
	}
}