using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using CodeChops.MagicEnums;
using CodeChops.MagicEnums.Attributes;

namespace CodeChops.Geometry.Space.Directions.Strict;

public abstract record StrictDirection<TDirectionMode> : StrictDirection<TDirectionMode, int>
	where TDirectionMode : StrictDirection<TDirectionMode>
{
}

/// <summary>
/// A strict direction based on a DirectionMode enum and therefore strongly typed. No freely direction delta points are used.
/// </summary>
[DisableConcurrency]
public record StrictDirection<TDirection, TDeltaPointNumber> : MagicCustomEnum<TDirection, Point<TDeltaPointNumber>>, IDirection<TDeltaPointNumber>, IStrictDirection
	where TDirection : StrictDirection<TDirection, TDeltaPointNumber>
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	public static ImmutableList<TDirection> Members { get; } = _members ??= GetEnumerable().ToImmutableList();
	private static readonly ImmutableList<TDirection> _members;

	public Point<float> GetValue() => this.Value.Cast<TDeltaPointNumber, float>();

	public static ImmutableList<TDirection> PossibleDirections { get; } = _possibleDirections ??= GetEnumerable().ToImmutableList();
	private static readonly ImmutableList<TDirection> _possibleDirections;

	/// <summary>
	/// RotationType left or right. Is non-deterministic!
	/// </summary>
	public TDirection GetDirectionFromRandomTurn()
	{
		var rotationType = (RotationType)(new Random().NextDouble() * 4 - 2);

		return this.GetDirectionFromTurn(rotationType);
	}

	public TDirection GetDirectionFromTurn(RotationType rotationType)
	{
		var currentDirectionTypeIndex = PossibleDirections.IndexOf((TDirection)this);
		var directionIndexDelta = rotationType == RotationType.Invert
			? PossibleDirections.Count / 2
			: (int)rotationType;
		var newDirectionTypeIndex = (currentDirectionTypeIndex + directionIndexDelta + PossibleDirections.Count) % PossibleDirections.Count;

		return PossibleDirections[newDirectionTypeIndex];
	}

	public TTargetDirection Cast<TTargetDirection>()
		where TTargetDirection : StrictDirection<TTargetDirection, TDeltaPointNumber>
	{
		return Cast<TTargetDirection>(this.Value);
	}

	public bool TryCast<TTargetDirection>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TDeltaPointNumber>
	{
		return TryCast(this.Value, out direction);
	}


	public static TTargetDirection Cast<TTargetDirection>(Point<TDeltaPointNumber> deltaPoint)
		where TTargetDirection : StrictDirection<TTargetDirection, TDeltaPointNumber>
	{
		if (!TryCast<TTargetDirection>(deltaPoint, out var direction))
		{
			throw new InvalidOperationException($"{deltaPoint} does not exist in {typeof(TTargetDirection).Name}.");
		}

		return direction;
	}

	public static bool TryCast<TTargetDirection>(Point<TDeltaPointNumber> deltaPoint, [NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TDeltaPointNumber>
	{
		if (!StrictDirection<TTargetDirection, TDeltaPointNumber>.TryGetSingleMember(deltaPoint, out var newDirection))
		{
			direction = null;
			return false;
		}

		direction = newDirection;
		return true;
	}
}
