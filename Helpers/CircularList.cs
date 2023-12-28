using System.Collections;

namespace Helpers;

public sealed class CircularList<T> : IEnumerable<CircularList<T>.Node>
{
	Node? _first;
	public Node First
	{
		get => _first ?? throw new InvalidOperationException("No elements");
		set => _first = value;
	}

	public CircularList(IEnumerable<T> items)
	{
		Node current;
		using var e = items.GetEnumerator();
		if(!e.MoveNext())
			throw new ArgumentException("Empty");

		First = current = new Node(e.Current);

		while(e.MoveNext())
		{
			var next = new Node(e.Current);
			next.Previous = current;
			current.Next = next;
			current = next;
		}

		First.Previous = current;
		current.Next = First;
	}

	public void RemoveNode(Node node)
	{
		if(ReferenceEquals(_first, node))
			_first = node.Next;

		if(ReferenceEquals(_first, node))
		{
			_first = null;
			return;
		}

		node.Remove();
	}

	public IEnumerator<CircularList<T>.Node> GetEnumerator()
	{
		if(_first is null)
			return Enumerable.Empty<CircularList<T>.Node>().GetEnumerator();

		return CircularListExtensions.EnumerateFrom(_first).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public sealed class Node
	{
		internal Node(T item)
		{
			Item = item;
			Next = this;
			Previous = this;
		}

		public T Item { get; private init; }
		public Node Next { get; internal set; }
		public Node Previous { get; internal set; }

		public void Remove()
		{
			Next.Previous = Previous;
			Previous.Next = Next;
			Next = this;
			Previous = this;
		}

		public void InsertAfter(T item)
		{
			var node = new Node(item);

			node.Next = Next;
			Next.Previous = node;

			node.Previous = this;
			Next = node;
		}

		public Node ReplaceWith(T item)
		{
			InsertAfter(item);
			var node = Next;

			Remove();
			return node;
		}
	}
}

public static class CircularListExtensions
{
	public static CircularList<T> ToCircularList<T>(this IEnumerable<T> enumerable) => new(enumerable);

	public static IEnumerable<CircularList<T>.Node> EnumerateFrom<T>(this CircularList<T>.Node node)
	{
		var first = node;
		var current = node;
		do
		{
			yield return current;
			current = current.Next;
		}
		while(!ReferenceEquals(current, first));
	}
}
