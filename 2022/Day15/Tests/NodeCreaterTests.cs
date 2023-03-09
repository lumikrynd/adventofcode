using Day15.Model;
using Day15.Parsing;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Day15.Tests;

internal class NodeCreaterTests
{
	[Test]
	public void SensorRangeCoveringNode_GetFullNode_1()
	{
		var sensor = new Coordinate(0, 0);
		var sensorRange = 0;
		var dimmensionSize = 1;

		var node = NodeCreater.CreateNode(sensor, sensorRange, dimmensionSize);
		node.Should().BeOfType<CoveredNode>();
	}

	[Test]
	public void SensorOutsideNode_GetEmptyNode()
	{
		var sensor = new Coordinate(-1, 0);
		var sensorRange = 0;
		var dimmensionSize = 1;

		var node = NodeCreater.CreateNode(sensor, sensorRange, dimmensionSize);
		node.Should().BeOfType<UncoveredNode>();
	}

	[TestCase(2, 2)]
	[TestCase(4, 6)]
	[TestCase(8, 14)]
	public void TestCoveringTime(int dimmensionSize, int sensorRange)
	{
		var sensor = new Coordinate(0, 0);

		var notCoveredNode = NodeCreater.CreateNode(sensor, sensorRange - 1, dimmensionSize);
		var coveredNode = NodeCreater.CreateNode(sensor, sensorRange, dimmensionSize);

		notCoveredNode.Should().NotBeOfType<CoveredNode>();
		coveredNode.Should().BeOfType<CoveredNode>();
	}
}
