using CodeChops.Geometry.Space;

namespace CodeChops.Geometry.UnitTests;

public class SizeTests
{
	[Theory]
	[InlineData(0,	 0																		)]
	[InlineData(1,	 0																		)]
	[InlineData(0,	 1																		)]
	[InlineData(1,	 2,  0,0,	0,1															)]
	[InlineData(2,	 1,  0,0,	1,0															)]
	[InlineData(2,	 2,  0,0,	1,0,	0,1,	1,1											)]
	[InlineData(3,   3,  0,0,	1,0,	2,0,	0,1,	1,1,	2,1,	0,2,	1,2,	2,2	)]		
	public void Size_Enumeration_IsCorrect(int width, int height, params int[] xOrY)
	{
		var size = new Size<int>(width, height);

		var points = size.GetEnumerable();
		foreach (var (index, point) in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
		}
		
		Assert.Equal(xOrY.Length / 2, points.Count());
	}
}