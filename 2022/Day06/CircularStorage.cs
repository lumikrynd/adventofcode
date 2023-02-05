using System.Collections;
using System.Collections.Generic;

namespace Day06;

internal class CircularStorage : IEnumerable<char>
{
	readonly char[] storage;

	int index = 0;

	public CircularStorage(int size)
	{
		storage = new char[size];
	}

	public void AddValue(char item)
	{
		storage[index++] = item;
		index %= storage.Length;
	}

	public IEnumerator<char> GetEnumerator()
	{
		foreach(var item in storage)
		{
			yield return item;
		}
	}

	public int Length => storage.Length;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
