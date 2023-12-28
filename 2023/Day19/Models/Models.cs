namespace Y2023.Day19.Models;

public record Workflow(string Name, List<Rule> Rules, string DefaultDestination);

public record Rule(Condition Condition, string Destination);

public record Condition(Category Category, Condition.Type Comparison, int Value)
{
	public enum Type
	{
		Bigger,
		Lesser,
	}
}

public record Part(int X, int M, int A, int S)
{
	public int GetCategoryValue(Category category) => category switch
	{
		Category.X => X,
		Category.M => M,
		Category.A => A,
		Category.S => S,
		_ => throw new NotImplementedException(),
	};
}

public enum Category
{
	/// <summary> Extremely cool looking </summary>
	X,

	/// <summary> Musical (it makes a noise when you hit it) </summary>
	M,

	/// <summary> Aerodynamic </summary>
	A,

	/// <summary> Shiny </summary>
	S,
}