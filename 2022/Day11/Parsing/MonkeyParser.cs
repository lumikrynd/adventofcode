using Day11.Models;

namespace Day11.Parsing;

public class MonkeyParser
{
	public static Monkey Parse(List<string> input)
	{
		var parser = new MonkeyParser(input);
		return parser.Parse();
	}

	private List<Item> items;
	private Func<int, int> modifier;
	private int divisor;
	private int trueTarget;
	private int falseTarget;

	private MonkeyParser(List<string> input)
	{
		items = ParseItems(input[1]);
		modifier = ParseModifier(input[2]);
		divisor = ParseDivisor(input[3]);
		trueTarget = ParseTarget(input[4]);
		falseTarget = ParseTarget(input[5]);
	}

	public Monkey Parse()
	{
		return new Monkey(items, modifier, divisor, trueTarget, falseTarget);
	}

	private List<Item> ParseItems(string input)
	{
		const string start = "Starting items: ";
		string temp = input.RemoveStart(start);
		var values = temp.Split(", ");
		return values
			.Select(ParseItem)
			.ToList();
	}

	private Item ParseItem(string input)
	{
		int value = int.Parse(input);
		return new Item() { WorryLevel = value };
	}

	private int ParseDivisor(string input)
	{
		const string start = "Test: divisible by ";
		string divisor = input.RemoveStart(start);
		return int.Parse(divisor);
	}

	private int ParseTarget(string input)
	{
		var words = input.Split();
		var target = words.Last();
		return int.Parse(target);
	}

	private Func<int, int> ParseModifier(string input)
	{
		const string start = "Operation: new = ";
		string expression = input.RemoveStart(start);
		return ExpressionParser.Parse(expression);
	}
}
