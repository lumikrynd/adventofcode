using System.Diagnostics.CodeAnalysis;

namespace Y2023.Day20.Models;

public record Module(string Name, ModuleType Type, List<string> Destinations);

public enum ModuleType
{
	None,
	FlipFlop,
	Conjunction,
}

public record ModuleState
{
	public required Dictionary<string, bool> FlipFlops { get; init; }
	public required Dictionary<string, Dictionary<string, bool>> Conjunctions { get; init; }

	public static ModuleState CreateFromModules(IEnumerable<Module> modules)
	{
		var flipFlops = CreateFlipFlops(modules);
		var conjunctions = CreateConjunctions(modules);

		return new ModuleState
		{
			FlipFlops = flipFlops,
			Conjunctions = conjunctions,
		};
	}

	private static Dictionary<string, bool> CreateFlipFlops(IEnumerable<Module> modules)
	{
		return modules
			.Where(m => m.Type == ModuleType.FlipFlop)
			.ToDictionary(m => m.Name, _ => false);
	}

	private static Dictionary<string, Dictionary<string, bool>> CreateConjunctions(IEnumerable<Module> modules)
	{
		var conjunctionNames = modules
			.Where(m => m.Type == ModuleType.Conjunction)
			.Select(m => m.Name)
			.ToHashSet();

		var inputs = modules
			.SelectMany(m => m.Destinations, (m, dest) => (Dest: dest, Source: m.Name))
			.GroupBy(pair => pair.Dest, pair => pair.Source);

		var conjunctions = inputs
			.Where(i => conjunctionNames.Contains(i.Key))
			.ToDictionary(i => i.Key, AddState);

		return conjunctions;
	}

	private static Dictionary<string, bool> AddState(IEnumerable<string> items)
		=> items.ToDictionary(n => n, _ => false);
}
