using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace CodeChops.Geometry.Space.Directions;

[StructLayout(LayoutKind.Auto)]
public readonly record struct NoDirection<TNumber> : IDirection<TNumber>
	where TNumber : INumber<TNumber>
{
	public static implicit operator Point<TNumber>(NoDirection<TNumber> value) => value.Value;
	
	public Point<TNumber> Value => Point<TNumber>.Default;

	public Point<TTargetNumber> Convert<TTargetNumber>()
		where TTargetNumber : INumber<TTargetNumber>
		=> Point<TTargetNumber>.Default;

	public static readonly NoDirection<TNumber> Instance = (NoDirection<TNumber>)FormatterServices.GetUninitializedObject(typeof(NoDirection<TNumber>));
	
	[Obsolete("Don't use this empty constructor. This is a singleton.", error: true)]
	public NoDirection() => throw new InvalidOperationException($"Don't use this empty constructor. This is a singleton.");
}