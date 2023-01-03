using CodeChops.ImplementationDiscovery;

namespace CodeChops.Geometry.Space.Directions.Strict;

[DiscoverImplementations(generateProxies: true)]
public partial interface IStrictDirection<TNumber> : IDirection<TNumber>, IStrictDirection
	where TNumber : INumber<TNumber>
{
	IStrictDirection<TNumber> GetDirectionFromRandomTurn(Random? random = null);
	IStrictDirection<TNumber> GetDirectionFromTurn(RotationType rotationType);
}

public interface IStrictDirection : IDirection, IMagicEnum
{
	bool TryGetDirection(string directionName, [NotNullWhen(true)] out IStrictDirection? direction);
	
	TTargetDirection Convert<TTargetDirection, TTargetNumber>()
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>;

	bool TryConvert<TTargetDirection, TTargetNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetNumber>, new()
		where TTargetNumber : INumber<TTargetNumber>;
}