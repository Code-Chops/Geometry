using System.Runtime.InteropServices;

namespace CodeChops.Geometry.Space.Sizes;

[StructLayout(LayoutKind.Auto)]
public struct SizeIterator<TNumber>
	where TNumber : INumber<TNumber>
{
	public Point<TNumber> Current { get; private set; }
	private Size<TNumber> Size { get; }
	private TNumber Address { get; set; }
	private TNumber MaxCount { get; }
	
	public SizeIterator(Size<TNumber> size)
	{
		this.Current = Point<TNumber>.Default;
		this.Size = size;
		this.Address = TNumber.Zero;
		this.MaxCount = this.Size.Area();
	}

	// ReSharper disable once UnusedMember.Global
	public bool MoveNext()
	{
		this.Address += TNumber.One;
		this.Current = new Point<TNumber>(this.Address, this.Size);
		return this.Address <= this.MaxCount;
	}
}