namespace Day20.Circular;

internal class LinkedItem<T>
{
	public T Value { get; private set; }
	public LinkedItem<T> Next { get; set; }
	public LinkedItem<T> Previous { get; set; }

	public LinkedItem(T value)
	{
		Value = value;
		Next = this;
		Previous = this;
	}

	public override string ToString()
	{
		return Value?.ToString() ?? "null";
	}
}
