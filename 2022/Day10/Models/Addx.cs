namespace Day10.Models;

internal class Addx : Instruction
{
	public int AddValue { get; }

	public Addx(int value)
	{
		AddValue = value;
	}

	int Instruction.TimeCost => 2;
	int Instruction.XChange => AddValue;
}
