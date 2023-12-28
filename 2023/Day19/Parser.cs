using Helpers;
using Y2023.Day19.Models;

namespace Y2023.Day19;

public class Parser : IDisposable
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static (List<Workflow> Workflows, List<Part> Parts) Parse(IEnumerable<string> lines)
	{
		using var parser = new Parser(lines);
		parser.Parse();
		return parser.Result();
	}

	EnumeratorWrapper<string> Input;
	List<Workflow> Workflows = new();
	List<Part> MachineParts = new();

	private Parser(IEnumerable<string> input)
	{
		Input = new(input);
	}

	private (List<Workflow> Workflows, List<Part> Parts) Result()
	{
		return (Workflows, MachineParts);
	}

	private void Parse()
	{
		while(Input.MoveNext().Trim() != "")
		{
			ParseWorkflow();
		}

		while(Input.TryMoveNext(out _))
		{
			ParsePart();
		}
	}

	private void ParseWorkflow()
	{
		var splitOn = new char[] { '{', '}', ',' };
		var parts = Input.Current.Split(splitOn, splitOptions);

		var name = parts[0];
		var defaultDestintion = parts[^1];
		var rules = parts[1..^1]
			.Select(ParseRule)
			.ToList();

		var workFlow = new Workflow(name, rules, defaultDestintion);
		Workflows.Add(workFlow);
	}

	private Rule ParseRule(string input)
	{
		var parts = input.Split(':', splitOptions);
		var condition = ParseCondition(parts[0]);
		var destination = parts[1];

		return new(condition, destination);
	}

	private Condition ParseCondition(string input)
	{
		var category = ParseCategory(input[0]);
		var type = ParseType(input[1]);
		var value = int.Parse(input[2..]);

		return new(category, type, value);
	}

	private Category ParseCategory(char category) => category switch
	{
		'x' or 'X' => Category.X,
		'm' or 'M' => Category.M,
		'a' or 'A' => Category.A,
		's' or 'S' => Category.S,
		_ => throw new NotImplementedException(),
	};

	private Condition.Type ParseType(char type) => type switch
	{
		'<' => Condition.Type.Lesser,
		'>' => Condition.Type.Bigger,
		_ => throw new NotImplementedException(),
	};

	private void ParsePart()
	{
		var splitOn = new char[] { '{', ',', '}' };
		var splits = Input.Current.Split(splitOn, splitOptions);

		var x = ParsePartCategory(splits[0], 'x');
		var m = ParsePartCategory(splits[1], 'm');
		var a = ParsePartCategory(splits[2], 'a');
		var s = ParsePartCategory(splits[3], 's');

		var part = new Part(x, m, a, s);
		MachineParts.Add(part);
	}

	private int ParsePartCategory(string input, char category)
	{
		var parts = input.Split('=');
		if(parts[0][0] != category)
			throw new Exception("Unexpected");

		return int.Parse(parts[1]);
	}

	public void Dispose()
	{
		Input.Dispose();
	}
}