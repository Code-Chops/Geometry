using CodeChops.ImplementationDiscovery;

namespace CodeChops.Geometry.Space.Directions.Strict;

[DiscoverImplementations]
public partial interface IStrictDirection<TNumber> : IDirection<TNumber>, IStrictDirection
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	IStrictDirection<TNumber> GetDirectionFromRandomTurn();
	IStrictDirection<TNumber> GetDirectionFromTurn(RotationType rotationType);
}

public interface IStrictDirection : IDirection, IMagicEnum
{
	bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection? direction);
	
	TTargetDirection Convert<TTargetDirection, TTargetNumber>()
			where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
			where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;

	bool TryConvert<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : struct, IComparable<TTargetNumber>, IEquatable<TTargetNumber>, IConvertible;
}