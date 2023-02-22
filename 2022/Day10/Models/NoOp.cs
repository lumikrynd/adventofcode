namespace Day10.Models;

internal class NoOp : Instruction
{
	public int TimeCost => 1;
	public int XChange => 0;
}
