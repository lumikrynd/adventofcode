using System.Diagnostics.CodeAnalysis;

namespace Helpers;

public class EnumeratorWrapper<T> : IDisposable
	where T : notnull
{
	private IEnumerator<T> Enumerator { get; }

	public T Current { get; private set; }
	private T? next;

	[MemberNotNullWhen(true, nameof(next))]
	public bool HasNext() => next != null;

	public EnumeratorWrapper(IEnumerator<T> enumerator)
	{
		Enumerator = enumerator;
		Enumerator.MoveNext();
		Current = Enumerator.Current;
		Enumerator.MoveNext();
		next = Enumerator.Current;
	}

	public EnumeratorWrapper(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator())
	{
	}

	public T GetNext()
	{
		if(!TryGetNext(out T? ret))
			throw new Exception("Check HasNext before use...");
		return ret;
	}

	public bool TryGetNext([NotNullWhen(true)] out T? value)
	{
		if(!TryPeek(out value))
			return false;
		MoveNext();
		return true;
	}

	public T Peek()
	{
		if(!TryPeek(out T? ret))
			throw new Exception("Check HasNext before use...");
		return ret;
	}

	public bool TryPeek([NotNullWhen(true)] out T? value)
	{
		value = next;
		return HasNext();
	}

	public void MoveNext()
	{
		if(!HasNext())
			throw new Exception("Check HasNext before use...");
		Current = next;
		Enumerator.MoveNext();
		next = Enumerator.Current;
	}

	public void Dispose()
	{
		Enumerator.Dispose();
	}
}