using System.Collections;

namespace Day20.Circular;

internal class CircularList<T> : IEnumerable<LinkedItem<T>>
{
	public LinkedItem<T> First { get; private set; }

	public CircularList(IEnumerable<T> values)
	{
		LinkedItem<T>? previous = null;

		foreach(T item in values)
		{
			var node = new LinkedItem<T>(item);
			if(previous != null)
			{
				node.Previous = previous;
				previous.Next = node;
			}
			previous = node;

			if(First == null)
			{
				First = node;
			}
		}

		if(First == null || previous == null)
			throw new Exception();

		First.Previous = previous;
		previous.Next = First;
	}

	public void MoveForward(LinkedItem<T> item)
	{
		CloseGap(item);
		MoveInAfter(item, item.Next);
	}

	public void MoveBackwardOriginal(LinkedItem<T> item)
	{
		CloseGap(item);
		MoveInAfter(item, item.Previous.Previous);
	}

	public void MoveBackward(LinkedItem<T> item)
	{
		CloseGap(item);
		MoveInAfter(item, item.Previous.Previous);
	}

	public void MoveInAfter(LinkedItem<T> item, LinkedItem<T> after)
	{
		var newNext = after.Next;

		item.Previous = after;
		after.Next = item;

		item.Next = newNext;
		newNext.Previous = item;
	}

	public void CloseGap(LinkedItem<T> item)
	{
		var curPre = item.Previous;
		var curNext = item.Next;

		curPre.Next = curNext;
		curNext.Previous = curPre;
	}

	public override string ToString()
	{
		IEnumerable<LinkedItem<T>> temp = this;
		var ret = string.Join(" ,", temp.Select(x => x.Value));
		return ret;
	}

	IEnumerator<LinkedItem<T>> IEnumerable<LinkedItem<T>>.GetEnumerator()
	{
		var current = First;
		yield return current;

		while(current.Next != First)
		{
			current = current.Next;
			yield return current;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		IEnumerable<LinkedItem<T>> temp = this;
		return temp.GetEnumerator();
	}
}
