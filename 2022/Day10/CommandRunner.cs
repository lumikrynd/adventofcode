using Day10.Models;

namespace Day10;

internal class CommandRunner
{
	int xValue = 1;
	int cycle = 0;
	int[] cycleXValues;
	int pointer = 1;

	public CommandRunner(List<Instruction> instructions)
	{
		cycleXValues = new int[240];
		foreach(var instruction in instructions)
		{
			ExecuteInstruction(instruction);
			if(FilledOutScreen)
				break;
		}
	}

	public int GetXValue(int cycle)
	{
		return cycleXValues[cycle - 1];
	}

	private void SetXValue(int cycle, int value)
	{
		cycleXValues[cycle - 1] = value;
	}

	private void ExecuteInstruction(Instruction instruction)
	{
		cycle += instruction.TimeCost;
		UpdateCycleValues();
		xValue += instruction.XChange;
	}

	private void UpdateCycleValues()
	{
		while(pointer <= cycle && !FilledOutScreen)
		{
			SetXValue(pointer, xValue);
			pointer++;
		}
	}

	bool FilledOutScreen => pointer > cycleXValues?.Length;
}
