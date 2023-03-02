using Day11.Models;

namespace Day11.Parsing;

public class MonkeyListParser
{
	public static List<Monkey> Parse(IEnumerable<string> input)
	{
		var parser = new MonkeyListParser(input.ToList());
		return parser.Parse();
	}

	List<Monkey> MonkeyList = new();

	private MonkeyListParser(List<string> input)
	{
		var monkeyNotes = SplitInput(input);
		foreach(var note in monkeyNotes)
		{
			AddMonkey(note);
		}
	}

	public List<Monkey> Parse()
	{
		return MonkeyList;
	}

	private static IEnumerable<List<string>> SplitInput(List<string> input)
	{
		const int noteLength = 7;
		for(int i = 0; i < input.Count; i += noteLength)
		{
			yield return input.Skip(i).Take(noteLength).ToList();
		}
	}

	private void AddMonkey(List<string> input)
	{
		var monkey = MonkeyParser.Parse(input);
		MonkeyList.Add(monkey);
	}
}
