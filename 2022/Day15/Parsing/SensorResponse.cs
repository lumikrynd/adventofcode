using Helpers;

namespace Day15.Parsing;

internal class SensorResponse
{
	public Coordinate Sensor { get; }
	public Coordinate Beacon { get; }

	public SensorResponse(Coordinate sensor, Coordinate beacon)
	{
		Sensor = sensor;
		Beacon = beacon;
	}
} 
