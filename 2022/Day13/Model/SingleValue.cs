using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13.Model;

internal class SingleValue : IValue
{
	public int Value { get; set; }

	public SingleValue(int value)
	{
		Value = value;
	}

	public int CompareTo(IValue? input)
	{
		if (input is SingleValue other)
			return Value - other.Value;
		else if (input is ValueList)
			return -input.CompareTo(this);

		throw new Exception();
	}

	public override string ToString()
	{
		return $"{Value}";
	}
}
