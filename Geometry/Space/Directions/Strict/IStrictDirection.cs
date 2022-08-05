﻿using CodeChops.ImplementationDiscovery;
using CodeChops.ImplementationDiscovery.Attributes;

namespace CodeChops.Geometry.Space.Directions.Strict;

[DiscoverImplementations]
public partial interface IStrictDirection<TNumber> : IDirection<TNumber>, IStrictDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	static abstract bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection<TNumber>? direction);
	IStrictDirection<TNumber> GetDirectionFromRandomTurn();
	IStrictDirection<TNumber> GetDirectionFromTurn(RotationType rotationType);
}

public interface IStrictDirection : IDirection, IMagicEnum
{
	string Name { get; }

	TTargetDirection Cast<TTargetDirection, TTargetNumber>()
			where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
			where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;

	bool TryCast<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;
}