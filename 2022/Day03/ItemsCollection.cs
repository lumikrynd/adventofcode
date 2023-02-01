using System;
using System.Collections.Generic;
using System.Linq;

namespace Day03;

internal class ItemsCollection
{
	private readonly string[] itemCollections;

	protected ItemsCollection(params string[] itemCollections)
	{
		this.itemCollections = itemCollections;
	}

	public int GetRearrangementPriority()
	{
		int priority = 0;
		var duplicateItems = GetIntersections();
		foreach(var item in duplicateItems)
		{
			priority += ItemPriority(item);
		}
		return priority;
	}

	private IEnumerable<char> GetIntersections()
	{
		IEnumerable<char> result = itemCollections[0];
		for(int i = 1; i < itemCollections.Length; i++)
		{
			result = result.Intersect(itemCollections[i]);
		}
		return result.Distinct();
	}

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