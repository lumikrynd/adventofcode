using Y2023.Day19.Models;

namespace Y2023.Day19;

public class Interpreter
{
	Dictionary<string, Workflow> Workflows;
	string start = "in";
	public Interpreter(IEnumerable<Workflow> workflows)
	{
		Workflows = workflows.ToDictionary(w => w.Name);
	}

	/// <returns>True if accepted, false if rejected. </returns>
	public bool SortPart(Part part)
	{
		var current = start;
		while(current is not ("A" or "R"))
		{
			var wf = Workflows[current];
			current = DoWorkflow(wf, part);
		}

		return current switch
		{
			"A" => true,
			"R" => false,
			_ => throw new NotImplementedException(),
		};
	}

	private string DoWorkflow(Workflow wf, Part part)
	{
		var rule = wf.Rules.FirstOrDefault(r => ConditionMet(r.Condition, part));
		return rule?.Destination ?? wf.DefaultDestination;
	}

	private bool ConditionMet(Condition condition, Part part)
	{
		var partValue = part.GetCategoryValue(condition.Category);

		return condition.Comparison switch
		{
			Condition.Type.Bigger => partValue > condition.Value,
			Condition.Type.Lesser => partValue < condition.Value,
			_ => throw new NotImplementedException(),
		};
	}
}