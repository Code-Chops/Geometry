using CodeChops.Geometry.Space.Points;
using Xunit.Abstractions;

namespace CodeChops.Geometry.UnitTests;

public class PointTests
{
	private ITestOutputHelper TestOutputHelper { get; }

	public PointTests(ITestOutputHelper testOutputHelper)
	{
		this.TestOutputHelper = testOutputHelper;
	}

	[InlineData(1,1,	-5,-5,		1,1,	0,0,	-1,-1,	-2,-2,	-3,-3,	-4,-4,	-5,-5)]
	[Theory]
	public void Iterator_ShouldBe_Correct(int startX, int startY, int endX, int endY, params int[] xOrY)
	{
		var startingPoint = new Point<double>(startX, startY);
		var endPoint = new Point<double>(endX, endY);

		var index = 0;
		foreach (var point in (startingPoint, endPoint))
		{
			this.TestOutputHelper.WriteLine(point.ToString());
			Assert.Equal(xOrY[index * 2], point.X.ConvertToPrimitive<int>());
			Assert.Equal(xOrY[index * 2 + 1], point.Y.ConvertToPrimitive<int>());
			index++;
		}
	}
}