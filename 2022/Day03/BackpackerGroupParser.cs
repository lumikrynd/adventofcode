using System.Collections.Generic;

namespace Day03;

internal class BackpackerGroupParser
{
	public static IEnumerable<BackpackerGroup> ParseBackpackerGroups(IEnumerable<string> backpackRawData)
	{
		var parser = new BackpackerGroupParser(backpackRawData);
		return parser.GetResult();
	}

	readonly IEnumerator<string> iterator;
	private bool more = true;
	private readonly List<BackpackerGroup> groups = new();

	private BackpackerGroupParser(IEnumerable<string> backpackRawData)
	{
		iterator = backpackRawData.GetEnumerator();
		iterator.MoveNext();
		Parse();
	}

	private void Parse()
	{
		while(more)
		{
			AddGroup();
		}
	}

	private void AddGroup()
	{
		var members = new[]
		{
			GetNext(),
			GetNext(),
			GetNext()
		};
		var group = new BackpackerGroup(members);
		groups.Add(group);
	}

	private string GetNext()
	{
		var current = iterator.Current;
		more = iterator.MoveNext();
		return current;
	}

	private IEnumerable<BackpackerGroup> GetResult() => groups;
}
