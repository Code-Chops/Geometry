using CodeChops.ImplementationDiscovery.Attributes;

namespace CodeChops.Geometry.Space.Directions.Strict;

[DiscoverImplementations(enumName: nameof(StrictDirectionEnum<TNumber>))]
public partial interface IStrictDirection<TNumber> : IDirection<TNumber>, IStrictDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	IStrictDirection<TNumber> GetDirectionFromRandomTurn();
	IStrictDirection<TNumber> GetDirectionFromTurn(RotationType rotationType);
}

public interface IStrictDirection : IDirection, IMagicEnum
{
	string Name { get; }

	bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection? direction);
	
	TTargetDirection Cast<TTargetDirection, TTargetNumber>()
			where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
			where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;

	bool TryCast<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;
}