using Day10.Models;

namespace Day10.Parsing;

internal class InstructionsParser
{
	public static List<Instruction> Parse(IEnumerable<string> rawData)
	{
		var parser = new InstructionsParser(rawData);
		return parser.Instructions;
	}

	public List<Instruction> Instructions { get; }

	private InstructionsParser(IEnumerable<string> rawData)
	{
		Instructions = new List<Instruction>();
		ParseInstructions(rawData);
	}

	private void ParseInstructions(IEnumerable<string> rawData)
	{
		foreach(var line in rawData)
		{
			var instruction = ParseInstruction(line);
			Instructions.Add(instruction);
		}
	}

	private Instruction ParseInstruction(string line)
	{
		var parts = line.Split();
		switch (parts[0])
		{
			case "noop":
				return ParseNoOp(parts);
			case "addx":
				return ParseAddx(parts);
			default:
				throw new NotImplementedException();
		}
	}

	private NoOp ParseNoOp(string[] parts)
	{
		return new NoOp();
	}

	private Addx ParseAddx(string[] parts)
	{
		int value = int.Parse(parts[1]);
		return new Addx(value);
	}
}
