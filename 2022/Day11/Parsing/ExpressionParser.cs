namespace Day11.Parsing;

public class ExpressionParser
{
	public static Func<long, long> Parse(string input)
	{
		var parser = new ExpressionParser(input);
		return parser.Parse();
	}

	private Func<long, long> arg1;
	private Func<long, long> arg2;
	private Func<long, long, long> operation;

	private ExpressionParser(string input)
	{
		var parts = input.Split();
		arg1 = ParseArgument(parts[0]);
		arg2 = ParseArgument(parts[2]);
		operation = ParseOperation(parts[1]);
	}

	public Func<long, long> Parse()
	{
		return x => operation(arg1(x), arg2(x));
	}

	private Func<long, long> ParseArgument(string arg)
	{
		if (arg == "old")
			return x => x;
		long result = long.Parse(arg);
		return x => result;
	}

	private Func<long, long, long> ParseOperation(string operation)
	{
		if (operation == "+")
			return (x, y) => x + y;
		if (operation == "*")
			return (x, y) => x * y;
		throw new NotImplementedException();
	}
}