using CodeChops.Geometry.Space;

namespace CodeChops.Geometry.UnitTests;

public class PointTests
{
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

		var points = startingPoint.GetPointsInDirection(direction, length);
		foreach (var (index, point) in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
		}
		
		Assert.Equal(xOrY.Length / 2, length);
	}
}