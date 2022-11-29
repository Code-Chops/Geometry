using CodeChops.MagicEnums.Core;

namespace CodeChops.Geometry.Space.Directions.Strict;

public abstract record StrictDirection<TSelf> : StrictDirection<TSelf, int>
	where TSelf : StrictDirection<TSelf>, new();

/// <summary>
/// A strict direction based on a StrictDirection magic enum and therefore strongly typed. No freely direction delta points are used.
/// </summary>
public abstract record StrictDirection<TSelf, TNumber> : MagicEnumCore<TSelf, Point<TNumber>>, IStrictDirection<TNumber>, IHasDefault<TSelf>
	where TSelf : StrictDirection<TSelf, TNumber>, new() where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	private static List<TSelf> PossibleDirections => _possibleDirections ??= GetMembers().ToList();
	private static List<TSelf>? _possibleDirections;

	public static TSelf Default { get; } = new();
	
	public Point<TTarget> GetValue<TTarget>()
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
	{
		return this.Value.Convert<TTarget>();
	}

	/// <summary>
	/// A publicly available wrapper of TryGetSingleMember. 
	/// </summary>
	public bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection? direction)
	{
		if (!StrictDirection<TSelf, TNumber>.TryGetSingleMember(directionName, out var concreteDirection))
		{
			direction = null;
			return false;
		}

		direction = concreteDirection!;
		return true;
	}

	protected static TSelf CreatePoint(int x, int y, [CallerMemberName] string name = null!)
	{
		var point = new Point<int>(x, y).Convert<TNumber>();
		var member = CreateMember<TSelf>(valueCreator: () => point, memberCreator: null, name);

		// Empty cache
		_possibleDirections = null;
		return member;
	}

	/// <summary>
	/// RotationType left or right. Is non-deterministic!
	/// </summary>
	public IStrictDirection<TNumber> GetDirectionFromRandomTurn()
	{
		var rotationType = (RotationType)(new Random().NextDouble() * 4 - 2);

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
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return Convert<TTargetDirection, TTargetNumber>(this.Value);
	}

	public bool TryConvert<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return TryConvert<TTargetDirection, TTargetNumber>(this.Value, out direction);
	}
	
	public static TTargetDirection Convert<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		if (!TryConvert<TTargetDirection, TTargetNumber>(deltaPoint, out var direction))
			throw new InvalidOperationException($"{deltaPoint} does not exist in {typeof(TTargetDirection).Name}.");

		return direction;
	}

	public static bool TryConvert<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint, [NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
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
}