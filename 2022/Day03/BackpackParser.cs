using System.Collections.Generic;

namespace Day03;

internal class BackpackParser
{
	public static IEnumerable<Backpack> ParseBackpacks(IEnumerable<string> backpackRawData)
	{
		List<Backpack> backpacks = new();
		foreach(string rawData in backpackRawData)
		{
			var backPack = CreateBackpack(rawData);
			backpacks.Add(backPack);
		}
		return backpacks;
	}

	private static Backpack CreateBackpack(string rawData)
	{
		(var first, var second) = SplitInHalfes(rawData);
		return new Backpack(first, second);
	}

	private static (string start, string end) SplitInHalfes(string s)
	{
		var halfLength = s.Length / 2;
		var start = s[..halfLength];
		var end = s[halfLength..];
		return (start, end);
	}
}
