using CodeChops.MagicEnums.Core;

namespace CodeChops.Geometry.Space.Directions.Strict;

public abstract record StrictDirection<TSelf> : StrictDirection<TSelf, int>
	where TSelf : StrictDirection<TSelf>, new();

/// <summary>
/// A strongly typed direction that resides in a specific enum that holds other direction values.
/// </summary>
public abstract record StrictDirection<TSelf, TNumber> : MagicEnumCore<TSelf, Point<TNumber>>, IStrictDirection<TNumber>, IHasDefault<TSelf>
	where TSelf : StrictDirection<TSelf, TNumber>, IStrictDirection<TNumber>, new() 
	where TNumber : INumber<TNumber>
{
	private static List<TSelf> PossibleDirections => _possibleDirections ??= GetMembers().ToList();
	private static List<TSelf>? _possibleDirections;

	public static TSelf Default { get; } = new();

	/// <summary>
	/// A publicly available wrapper of TryGetSingleMember. 
	/// </summary>
	public bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection? direction)
	{
		if (!TryGetSingleMember(directionName, out var concreteDirection))
		{
			direction = null;
			return false;
		}

		direction = concreteDirection;
		return true;
	}
	
	private static Random Random { get; } = new();
	public IStrictDirection<TNumber> GetDirectionFromRandomTurn(Random? random = null)
	{
		var rotationType = (RotationType)((random ?? Random).NextDouble() * 4 - 2);

		return this.GetDirectionFromTurn(rotationType);
	}
	
	public IStrictDirection<TNumber> GetDirectionFromTurn(RotationType rotationType)
	{
		var currentDirectionTypeIndex = PossibleDirections.IndexOf((TSelf)this);
		var directionIndexDelta = rotationType == RotationType.Invert
			? PossibleDirections.Count / 2
			: (int)rotationType;
		var newDirectionTypeIndex = (currentDirectionTypeIndex + directionIndexDelta + PossibleDirections.Count) % PossibleDirections.Count;

		return PossibleDirections[newDirectionTypeIndex];
	}

	public TTargetDirection Convert<TTargetDirection, TTargetNumber>()
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>
	{
		return Convert<TTargetDirection, TTargetNumber>(this.Value);
	}

	public bool TryConvert<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>
	{
		return TryConvert<TTargetDirection, TTargetNumber>(this.Value, out direction);
	}
	
	public static TTargetDirection Convert<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>
	{
		if (!TryConvert<TTargetDirection, TTargetNumber>(deltaPoint, out var direction))
			throw new InvalidOperationException($"{deltaPoint} does not exist in {typeof(TTargetDirection).Name}.");

		return direction;
	}

	public static bool TryConvert<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint, [NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>
	{
		var newDeltaPoint = deltaPoint.Convert<TTargetNumber>();
		if (!StrictDirection<TTargetDirection, TTargetNumber>.TryGetSingleMember(newDeltaPoint, out var newDirection))
		{
			direction = null;
			return false;
		}

		direction = newDirection;
		return true;
	}
	
	protected static TSelf CreatePoint<TSourceNumber>(TSourceNumber x, TSourceNumber y, [CallerMemberName] string name = null!)
		where TSourceNumber : INumber<TSourceNumber>
	{
		var point = Point<TNumber>.Create(x, y);
		var member = CreateMember<TSelf>(valueCreator: () => point, memberCreator: null, name);

		// Empty cache
		_possibleDirections = null;
		return member;
	}
	
	public Point<TTargetNumber> GetDeltaPoint<TTargetNumber>() 
		where TTargetNumber : INumber<TTargetNumber>
		=> this.Value.Convert<TTargetNumber>();
}