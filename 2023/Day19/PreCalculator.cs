using Y2023.Day19.Models;

namespace Y2023.Day19;

public class PreCalculator
{
	Dictionary<string, Workflow> Workflows;
	string start = "in";

	public PreCalculator(IEnumerable<Workflow> workflows)
	{
		Workflows = workflows.ToDictionary(w => w.Name);
	}

	public List<PartRange> CalculateAccepted(PartRange range)
	{
		var accepted = new List<PartRange>();
		var ranges = new Stack<WorkItem>();
		ranges.Push(new(start, range));

		while(ranges.TryPop(out var item))
		{
			if(item.Workflow is "A")
				accepted.Add(item.Range);

			if(item.Workflow is "R" or "A")
				continue;

			var newItems = SplitByWorkflow(item);
			foreach(var n in newItems)
			{
				ranges.Push(n);
			}
		}

		return accepted;
	}

	private IEnumerable<WorkItem> SplitByWorkflow(WorkItem item)
	{
		var workflow = Workflows[item.Workflow];
		var range = item.Range;

		foreach(var rule in workflow.Rules)
		{
			var split = Split(range, rule.Condition);
			if(split.met != null)
				yield return new(rule.Destination, split.met);

			if(split.other is null)
				yield break;

			range = split.other;
		}

		yield return new(workflow.DefaultDestination, range);
	}

	private (PartRange? met, PartRange? other) Split(PartRange range, Condition con)
	{
		int splitValue = con.Value;
		splitValue += con.Comparison == Condition.Type.Bigger ? 1 : 0;

		var (a, b) = range.SplitAt(con.Category, splitValue);

		if(con.Comparison == Condition.Type.Bigger)
			(a, b) = (b, a);

		return (a, b);
	}

	private readonly record struct WorkItem(string Workflow, PartRange Range);
}