using System.Diagnostics.CodeAnalysis;

namespace CodeChops.Geometry.Space.Directions.Strict;

public interface IStrictDirection<TDeltaPointNumber> : IDirection<TDeltaPointNumber>, IStrictDirection
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	IStrictDirection<TDeltaPointNumber> GetDirectionFromRandomTurn();
	IStrictDirection<TDeltaPointNumber> GetDirectionFromTurn(RotationType rotationType);
}

public interface IStrictDirection : IDirection
{
	string Name { get; }

	TTargetDirection Cast<TTargetDirection, TTargetDeltaPointNumber>()
			where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
			where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible;

	bool TryCast<TTargetDirection, TTargetDeltaPointNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible;
}