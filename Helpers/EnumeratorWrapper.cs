using System.Diagnostics.CodeAnalysis;

namespace Helpers;

public class EnumeratorWrapper<T> : IDisposable
	where T : notnull
{
	private IEnumerator<T> Enumerator { get; }

	public T Current => current ?? throw new Exception("Not initialized");
	private T? current;
	private T? next;

	[MemberNotNullWhen(true, nameof(next))]
	public bool HasNext() => next != null;

	public EnumeratorWrapper(IEnumerator<T> enumerator)
	{
		Enumerator = enumerator;
		Enumerator.MoveNext();
		next = Enumerator.Current;
	}

	public EnumeratorWrapper(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator())
	{
	}

	public static EnumeratorWrapper<T> WithInitializedCurrent(IEnumerable<T> enumerable)
	{
		return WithInitializedCurrent(enumerable.GetEnumerator());
	}

	public static EnumeratorWrapper<T> WithInitializedCurrent(IEnumerator<T> enumerator)
	{
		var wrapper = new EnumeratorWrapper<T>(enumerator);
		wrapper.MoveNext();
		return wrapper;
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

	public bool TryMoveNext([NotNullWhen(true)] out T? value)
	{
		if(!TryPeek(out value))
			return false;

		current = value;
		Enumerator.MoveNext();
		next = Enumerator.Current;
		return true;
	}

	public T MoveNext()
	{
		if(!TryMoveNext(out var ret))
			throw new Exception("Check HasNext before use...");
		return ret;
	}

	public void Dispose()
	{
		Enumerator.Dispose();
	}
}