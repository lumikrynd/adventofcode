namespace Day11.Parsing;

public class ExpressionParser
{
	public static Func<int, int> Parse(string input)
	{
		var parser = new ExpressionParser(input);
		return parser.Parse();
	}

	private Func<int, int> arg1;
	private Func<int, int> arg2;
	private Func<int, int, int> operation;

	private ExpressionParser(string input)
	{
		var parts = input.Split();
		arg1 = ParseArgument(parts[0]);
		arg2 = ParseArgument(parts[2]);
		operation = ParseOperation(parts[1]);
	}

	public Func<int, int> Parse()
	{
		return x => operation(arg1(x), arg2(x));
	}

	private Func<int, int> ParseArgument(string arg)
	{
		if (arg == "old")
			return x => x;
		int result = int.Parse(arg);
		return x => result;
	}

	private Func<int, int, int> ParseOperation(string operation)
	{
		if (operation == "+")
			return (x, y) => x + y;
		if (operation == "*")
			return (x, y) => x * y;
		throw new NotImplementedException();
	}
}