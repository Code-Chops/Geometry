using CodeChops.Geometry.Space.Lines;
using CodeChops.Geometry.Space.Points;
using Xunit.Abstractions;

namespace CodeChops.Geometry.UnitTests;

public class LineTests
{
	private ITestOutputHelper TestOutputHelper { get; }

	public LineTests(ITestOutputHelper testOutputHelper)
	{
		this.TestOutputHelper = testOutputHelper;
	}

	[InlineData(1,1,	-5,-5,		1,1,	0,0,	-1,-1,	-2,-2,	-3,-3,	-4,-4,	-5,-5)]
	[Theory]
	public void Iterator_ShouldBe_Correct(int startX, int startY, int endX, int endY, params int[] xOrY)
	{
		var startingPoint = new Point<double>(startX, startY);
		var endPoint = new Point<double>(endX, endY);
		var line = new Line<double>(startingPoint, endPoint);
		
		var index = 0;
		foreach (var point in line)
		{
			this.TestOutputHelper.WriteLine(line.ToString());
			Assert.Equal(xOrY[index * 2], Math.Round(point.X, MidpointRounding.AwayFromZero));
			Assert.Equal(xOrY[index * 2 + 1], Math.Round(point.Y, MidpointRounding.AwayFromZero));
			index++;
		}
	}
}