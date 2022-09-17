using CodeChops.Geometry.Space;

namespace CodeChops.Geometry.UnitTests;

public class SurfaceTests
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
		var surface = new Surface<int>(size);
		
		var points = surface.GetAllPoints().ToList();
		foreach (var (index, point) in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
		}
		
		Assert.Equal(xOrY.Length / 2, points.Count());
	}
	
	[Theory]
	[InlineData(0,10,	nameof(EveryDirection.North),		3,		0,10,	0,9,	0,8 		)]
	[InlineData(1,0,	nameof(EveryDirection.NorthEast),	0									)]	
	[InlineData(0,1,	nameof(EveryDirection.East),		4,		0,1,	1,1,	2,1,	3,1	)]
	[InlineData(1,2,	nameof(EveryDirection.SouthEast),	1,		1,2							)]
	[InlineData(2,1,	nameof(EveryDirection.South),		2,		2,1,	2,2					)]
	[InlineData(2,2,	nameof(EveryDirection.SouthWest),	3,		2,2,	1,3,	0,4			)]
	[InlineData(1,5,	nameof(EveryDirection.West),		4,		1,5,	0,5,	-1,5,	-2,5)]		
	[InlineData(1,1,	nameof(EveryDirection.NorthWest),	3,		1,1,	0,0,	-1,-1		)]		
	public void Point_Enumeration_IsCorrect(int x, int y, string directionName, int length, params int[] xOrY)
	{
		var startingPoint = new Point<int>(x, y);
		var direction = EveryDirection.GetSingleMember(directionName);
		var surface = new Surface<int>(size: (10,10), offset: (-5,-5));
		
		var points = surface.GetPointsInDirection(startingPoint, direction, length);
		foreach (var (index, point) in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
		}
		
		Assert.Equal(xOrY.Length / 2, length);
	}
}