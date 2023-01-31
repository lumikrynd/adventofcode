using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day03;

internal class Backpack : ItemsCollection
{
	public Backpack(string firstCompartment, string secondCompartment) : base(firstCompartment, secondCompartment)
	{
	}
}
