using Day11.Models;

namespace Day11.Parsing;

public class MonkeyParser : IParser<List<string>, Monkey>
{
	IParser<string, Func<long, long>> _expressionParser;
	public MonkeyParser(IParser<string, Func<long, long>> expressionParser)
	{
		_expressionParser = expressionParser;
	}

	public Monkey Parse(List<string> input)
	{
		var items = ParseItems(input[1]);
		var modifier = ParseModifier(input[2]);
		var divisor = ParseDivisor(input[3]);
		var trueTarget = ParseTarget(input[4]);
		var falseTarget = ParseTarget(input[5]);

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
		var value = long.Parse(input);
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

	private Func<long, long> ParseModifier(string input)
	{
		const string start = "Operation: new = ";
		string expression = input.RemoveStart(start);
		return _expressionParser.Parse(expression);
	}
}
