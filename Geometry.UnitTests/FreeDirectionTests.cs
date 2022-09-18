using CodeChops.Geometry.Space.Points;

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
		var (x, y) = direction.GetValue<float>();
		var expectedPoint = new Point<float>(expectedDeltaX, expectedDeltaY);

		Assert.Equal((int)expectedPoint.X, (int)x);
		Assert.Equal((int)expectedPoint.Y, (int)y);

		direction = new(expectedPoint);

		Assert.Equal(angle, direction.Angle);
	}
}