namespace CodeChops.Geometry.Space.Movements.Steps;

public interface IStepCounter
{
	public long Steps { get; }
	public bool IsRunning { get; }
	public void Start();
	public void Stop();
	public void Restart();	
}