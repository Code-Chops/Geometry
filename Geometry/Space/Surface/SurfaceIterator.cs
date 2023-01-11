using System.Runtime.InteropServices;
using CodeChops.Geometry.Space.Lines;

namespace CodeChops.Geometry.Space.Surface;

[StructLayout(LayoutKind.Auto)]
public struct SurfaceIterator<TNumber>
	where TNumber : INumber<TNumber>
{
	public Point<TNumber> Current { get; private set; }

	private TNumber Count { get; set; }
	private TNumber Distance { get; }
	private Point<TNumber> Delta { get; }
	
	public SurfaceIterator(Line<TNumber> line)
	{
		var difference = line.Difference();
		this.Distance = TNumber.CreateChecked(line.Distance() - 1);
		this.Delta = difference / (this.Distance - TNumber.One);
		this.Current = line.StartingPoint - this.Delta;
		this.Count = TNumber.Zero;
	}

	// ReSharper disable once UnusedMember.Global
	public bool MoveNext()
	{
		this.Current += this.Delta;
		this.Count++;
		
		return this.Count <= this.Distance;
	}
}