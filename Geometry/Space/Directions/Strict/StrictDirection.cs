using CodeChops.MagicEnums.Core;

namespace CodeChops.Geometry.Space.Directions.Strict;

public abstract record StrictDirection<TSelf> : StrictDirection<TSelf, int>
	where TSelf : StrictDirection<TSelf>;

/// <summary>
/// A strict direction based on a StrictDirection magic enum and therefore strongly typed. No freely direction delta points are used.
/// </summary>
public abstract record StrictDirection<TSelf, TNumber> : MagicEnumCore<TSelf, Point<TNumber>>, IStrictDirection<TNumber>
	where TSelf : StrictDirection<TSelf, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	private static List<TSelf> PossibleDirections => _possibleDirections ??= GetEnumerable().ToList();
	private static List<TSelf>? _possibleDirections;
	
	public Point<TTargetNumber> GetValue<TTargetNumber>()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return this.Value.Cast<TNumber, TTargetNumber>();
	}

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

	protected static TSelf CreatePoint(int x, int y, [CallerMemberName] string name = null!)
	{
		var point = new Point<int>(x, y).Cast<TNumber>();
		var member = CreateMember(point, name);

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

	public TTargetDirection Cast<TTargetDirection, TTargetNumber>()
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return Cast<TTargetDirection, TTargetNumber>(this.Value);
	}

	public bool TryCast<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		return TryCast<TTargetDirection, TTargetNumber>(this.Value, out direction);
	}
	
	public static TTargetDirection Cast<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		if (!TryCast<TTargetDirection, TTargetNumber>(deltaPoint, out var direction))
		{
			throw new InvalidOperationException($"{deltaPoint} does not exist in {typeof(TTargetDirection).Name}.");
		}

		return direction;
	}

	public static bool TryCast<TTargetDirection, TTargetNumber>(Point<TNumber> deltaPoint, [NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible
	{
		var newDeltaPoint = deltaPoint.Cast<TTargetNumber>();
		if (!StrictDirection<TTargetDirection, TTargetNumber>.TryGetSingleMember(newDeltaPoint, out var newDirection))
		{
			direction = null;
			return false;
		}

		direction = newDirection;
		return true;
	}
}