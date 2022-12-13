using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12.Pathfinding;

internal class Heap<T> where T : class, IHeapElement
{
	private readonly List<T> Content = new();

	public int Count => Content.Count;

	public void Add(T item)
	{
		Content.Add(item);
		int index = Content.Count;

		int parrent = Parrent(index);

		while(Content[index].Weight < Content[parrent].Weight)
		{
			(Content[index], Content[parrent]) = (Content[parrent], Content[index]);
		}
	}

	public T Pop()
	{
		if (Content.Count == 0)
			throw new Exception();

		T ret = Content[0];

		var last = Content.Count - 1;
		Content[0]  = Content[last];
		Content.RemoveAt(last);

		var index = 0;
		while(LChild(index) < Content.Count)
		{
			var candidate = LChild(index);
			var rIndex = RChild(index);
			if(rIndex < Content.Count)
			{
				if (Weight(rIndex) < Weight(candidate))
				{
					candidate = rIndex;
				}
			}

			if (Weight(candidate) >= Weight(index))
				break;

			(Content[index], Content[candidate]) = (Content[candidate], Content[index]);
			index = candidate;
		}

		return ret;
	}

	int Weight(int index) => Content[index].Weight;

	static int Parrent(int index) => (index - 1) / 2;
	static int LChild(int index) => (index * 2) + 1;
	static int RChild(int index) => (index * 2) + 2;
}

internal interface IHeapElement
{
	public int Weight { get; }
} 
