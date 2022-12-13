using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13.Model;

internal class ValueList : IValue
{
	public List<IValue> Values { get; set; } = new();

	public int CompareTo(IValue? input)
	{
		ValueList other = ToValueLIst(input);

		var compareTo = Math.Min(Values.Count, other.Values.Count);

		for(int i = 0; i < compareTo; i++)
		{
			var result = Values[i].CompareTo(other.Values[i]);

			if(result != 0)
				return result;
		}

		return Values.Count - other.Values.Count;
	}

	private static ValueList ToValueLIst(IValue? input)
	{
		if (input is ValueList list)
		{
			return list;
		}
		else if (input is SingleValue value)
		{
			var temp = new ValueList();
			temp.Values.Add(value);

			return temp;
		}
		else
		{
			throw new Exception();
		}
	}

	public override string ToString()
	{
		return '[' + string.Join(',', Values) + ']';
	}
}
