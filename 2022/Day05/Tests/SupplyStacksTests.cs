using System;
using System.Collections.Generic;
using System.Linq;
using Day05.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Day05;

public class SupplyStacksTests
{
	[Test]
	public void TryingToUnderstandStacksEnumerableInteraction()
	{
		List<char> items = new() { 'a', 'b', 'c', 'd' };
		var crane = new OldCrane();
		Stack<char> stack = new(items);

		foreach(var item in stack)
		{
			Console.WriteLine(item);
		}

		foreach(var item in items)
		{
			Console.WriteLine(item);
		}
	}

	[Test]
	public void Init_FromList_FirstItemTop()
	{
		List<char> items = new() { 'a', 'b', 'c', 'd' };
		var crane = new OldCrane();
		SupplyStacks s = new(crane, new[] { items });

		s.PeekStack(1).Should().Be(items.First());
	}

	[Test]
	public void MoveStack_OldCrane_StackReverts()
	{
		List<char> items = new() { 'a', 'b', 'c', 'd' };
		var crane = new OldCrane();
		SupplyStacks stack = new(crane, new[] { items, Enumerable.Empty<char>() });

		Move move = new(4, 1, 2);

		stack.Move(move);

		stack.PeekStack(2).Should().Be(items.Last());
	}

	[Test]
	public void MoveStack_NewCrane_StackKeepOrder()
	{
		List<char> items = new() { 'a', 'b', 'c', 'd' };
		var crane = new StackCrane();
		SupplyStacks stack = new(crane, new[] { items, Enumerable.Empty<char>() });

		Move move = new(4, 1, 2);

		stack.Move(move);

		stack.PeekStack(2).Should().Be(items.First());
	}
}