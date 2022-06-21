using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CodeChops.MagicEnums;
using CodeChops.MagicEnums.Attributes;

namespace CodeChops.Geometry.Space.Directions.Strict;

public abstract record StrictDirection<TDirection> : StrictDirection<TDirection, int>
	where TDirection : StrictDirection<TDirection>
{
}

/// <summary>
/// A strict direction based on a StrictDirection magic enum and therefore strongly typed. No freely direction delta points are used.
/// </summary>
[DisableConcurrency]
public abstract record StrictDirection<TDirection, TNumber> : MagicCustomEnum<TDirection, Point<TNumber>>, IStrictDirection<TNumber>
	where TDirection : StrictDirection<TDirection, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static ImmutableList<TDirection> Members { get; } = _members ??= GetEnumerable().ToImmutableList();
	private static readonly ImmutableList<TDirection> _members;

	public Point<TTargetPointNumber> GetValue<TTargetPointNumber>()
		where TTargetPointNumber : struct, IComparable<TTargetPointNumber>, IEquatable<TTargetPointNumber>, IConvertible
	{
		return this.Value.Cast<TNumber, TTargetPointNumber>();
	}

	public static ImmutableList<TDirection> PossibleDirections { get; } = _possibleDirections ??= GetEnumerable().ToImmutableList();
	private static readonly ImmutableList<TDirection> _possibleDirections;

	public static TDirection CreateMember(int x, int y, [CallerMemberName] string name = null!)
{
		var point = new Point<int>(x, y).Cast<TNumber>();
		var member = CreateMember(point, name);
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
		var currentDirectionTypeIndex = PossibleDirections.IndexOf((TDirection)this);
		var directionIndexDelta = rotationType == RotationType.Invert
			? PossibleDirections.Count / 2
			: (int)rotationType;
		var newDirectionTypeIndex = (currentDirectionTypeIndex + directionIndexDelta + PossibleDirections.Count) % PossibleDirections.Count;

		return PossibleDirections[newDirectionTypeIndex];
	}

	public TTargetDirection Cast<TTargetDirection, TTargetDeltaPointNumber>()
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible
	{
		return Cast<TTargetDirection, TTargetDeltaPointNumber>(this.Value);
	}

	public bool TryCast<TTargetDirection, TTargetDeltaPointNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible
	{
		return TryCast<TTargetDirection, TTargetDeltaPointNumber>(this.Value, out direction);
	}


	public static TTargetDirection Cast<TTargetDirection, TTargetDeltaPointNumber>(Point<TNumber> deltaPoint)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible
	{
		if (!TryCast<TTargetDirection, TTargetDeltaPointNumber>(deltaPoint, out var direction))
		{
			throw new InvalidOperationException($"{deltaPoint} does not exist in {typeof(TTargetDirection).Name}.");
		}

		return direction;
	}

	public static bool TryCast<TTargetDirection, TTargetDeltaPointNumber>(Point<TNumber> deltaPoint, [NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible
	{
		var newDeltaPoint = deltaPoint.Cast<TTargetDeltaPointNumber>();
		if (!StrictDirection<TTargetDirection, TTargetDeltaPointNumber>.TryGetSingleMember(newDeltaPoint, out var newDirection))
		{
			direction = null;
			return false;
		}

		direction = newDirection;
		return true;
	}
}