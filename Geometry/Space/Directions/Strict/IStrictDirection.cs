using System.Diagnostics.CodeAnalysis;

namespace CodeChops.Geometry.Space.Directions.Strict;

public interface IStrictDirection<TDeltaPointNumber> : IDirection<TDeltaPointNumber>
	where TDeltaPointNumber : struct, IComparable<TDeltaPointNumber>, IEquatable<TDeltaPointNumber>, IConvertible
{
	string Name { get; }

	IStrictDirection<TDeltaPointNumber> GetDirectionFromRandomTurn();
	IStrictDirection<TDeltaPointNumber> GetDirectionFromTurn(RotationType rotationType);

	TTargetDirection Cast<TTargetDirection, TTargetDeltaPointNumber>()
			where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
			where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible;

	bool TryCast<TTargetDirection, TTargetDeltaPointNumber>([NotNullWhen(true)] out TTargetDirection? direction)
		where TTargetDirection : StrictDirection<TTargetDirection, TTargetDeltaPointNumber>
		where TTargetDeltaPointNumber : struct, IComparable<TTargetDeltaPointNumber>, IEquatable<TTargetDeltaPointNumber>, IConvertible;
}