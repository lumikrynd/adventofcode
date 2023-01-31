using System;

namespace Day03;

internal static class PriorityCalculater
{
	public static int ItemPriority(char item)
	{
		return item switch
		{
			(>= 'a' and <= 'z') => (item - 'a' + 1),
			(>= 'A' and <= 'Z') => (item - 'A' + 27),
			_ => throw new NotImplementedException(),
		};
	}
}