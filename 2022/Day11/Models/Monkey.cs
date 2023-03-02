namespace Day11.Models;

public class Monkey
{
	List<Item> Items { get; set; }
	Func<int, int> WorryModifier { get; }
	int DivisorTest { get; }
	int TrueTarget { get; }
	int FalseTarget { get; }
	public int InspectCount { get; private set; } = 0;

	public Monkey(List<Item> items, Func<int, int> modifier, int divisor, int trueTarget, int falseTarget)
	{
		Items = items;
		WorryModifier= modifier;
		DivisorTest = divisor;
		TrueTarget = trueTarget;
		FalseTarget = falseTarget;
	}

	public void InspectItems()
	{
		foreach(var item in Items)
		{
			Inspect(item);
		}
	}

	private void Inspect(Item item)
	{
		int newValue = WorryModifier(item.WorryLevel);
		item.WorryLevel = newValue;
		InspectCount++;
	}

	public void PostInspectRelief()
	{
		foreach(var item in Items)
		{
			item.WorryLevel /= 3;
		}
	}

	public void ThrowItems(List<Monkey> monkeys)
	{
		var toThrow = Items;
		Items = new();
		foreach(var item in toThrow)
		{
			ThrowItem(monkeys, item);
		}
	}

	private void ThrowItem(List<Monkey> monkeys, Item item)
	{
		if(TestItem(item))
			monkeys[TrueTarget].Items.Add(item);
		else
			monkeys[FalseTarget].Items.Add(item);
	}

	private bool TestItem(Item item)
	{
		return item.WorryLevel % DivisorTest == 0;
	}
}
