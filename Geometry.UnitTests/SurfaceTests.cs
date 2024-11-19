using CodeChops.DomainModeling.Serialization.Json;
using CodeChops.Geometry.Space.Points;
using CodeChops.Geometry.Space.Sizes;
using CodeChops.Geometry.Space.Surface;
using CodeChops.MagicEnums.Json;

namespace CodeChops.Geometry.UnitTests;

public class SurfaceTests
{
	static SurfaceTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new MagicEnumJsonConverterFactory());
		JsonSerialization.DefaultOptions.Converters.Add(new ValueObjectJsonConverterFactory());
	}

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

		var points = surface.Size.GetAllPoints().ToList();
		var index = 0;
		foreach (var point in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
			index++;
		}

		Assert.Equal(xOrY.Length / 2, points.Count);
	}

	[Theory]
	[InlineData(0,7,	nameof(EveryDirection.North),		3,		0,7,	0,6,	0,5 		)]
	[InlineData(1,0,	nameof(EveryDirection.South),		0,		1,0,	1,1,	1,2,	1,3 )]
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
		var surface = new Surface<int>(size: (20,20), offset: (-10,-10));

		var points = surface.GetPointsInDirection(startingPoint, direction, length);
		var index = 0;
		foreach (var point in points)
		{
			var expectedPoint = new Point<int>(xOrY[index * 2], xOrY[index * 2 + 1]);
			Assert.Equal(expectedPoint, point);
			index++;
		}

		if (length > 0)
			Assert.Equal(xOrY.Length / 2, length);
	}
}