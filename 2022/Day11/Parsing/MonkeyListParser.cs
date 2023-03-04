using Day11.Models;

namespace Day11.Parsing;

public class MonkeyListParser : IParser<List<string>, List<Monkey>>
{
	public static MonkeyListParser CreateParser()
	{
		var expressionParser = new ExpressionParser();
		var monkeyParser = new MonkeyParser(expressionParser);
		return new MonkeyListParser(monkeyParser);
	}

	IParser<List<string>, Monkey> _monkeyParser;

	public MonkeyListParser(IParser<List<string>, Monkey> monkeyParser)
	{
		_monkeyParser = monkeyParser;
	}

	public List<Monkey> Parse(List<string> input)
	{
		var monkeyList = new List<Monkey>();
		var monkeyNotes = SplitInput(input);
		foreach(var note in monkeyNotes)
		{
			var monkey = _monkeyParser.Parse(note);
			monkeyList.Add(monkey);
		}
		return monkeyList;
	}

	private static IEnumerable<List<string>> SplitInput(List<string> input)
	{
		const int noteLength = 7;
		for(int i = 0; i < input.Count; i += noteLength)
		{
			yield return input.Skip(i).Take(noteLength).ToList();
		}
	}
}
