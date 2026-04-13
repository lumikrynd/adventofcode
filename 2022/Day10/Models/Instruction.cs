namespace Y2022.Day10.Models;

internal interface Instruction
{
	int TimeCost { get; }
	int XChange { get; }
}