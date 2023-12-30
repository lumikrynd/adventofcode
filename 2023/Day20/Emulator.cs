using Y2023.Day20.Models;

namespace Y2023.Day20;

public class Emulator
{
	Dictionary<string, Module> Modules;
	ModuleState State;

	int LowCount = 0;
	int HighCount = 0;

	public Emulator(List<Module> modules)
	{
		Modules = modules.ToDictionary(m => m.Name);
		State = ModuleState.CreateFromModules(modules);
	}

	public (int Low, int High) GetCounts() => (LowCount, HighCount);

	public void PushButton(int times)
	{
		for(int i = 0; i < times; i++)
		{
			PushButton();
		}
	}

	public void PushButton()
	{
		var pulses = ButtonPulse();
		while(pulses.TryDequeue(out var pulse))
		{
			var nextPulses = DoPulse(pulse);
			foreach(var p in nextPulses)
				pulses.Enqueue(p);
		}
	}

	private Queue<Pulse> ButtonPulse()
	{
		const string start = "broadcaster";
		LowCount++;

		var pulses = Modules[start].Destinations
			.Select(m => new Pulse(false, m, start));
		return new(pulses);
	}

	private IEnumerable<Pulse> DoPulse(Pulse pulse)
	{
		AddToCunt(pulse.Signal);

		if(!Modules.TryGetValue(pulse.Target, out var module))
			return Enumerable.Empty<Pulse>();

		switch(module.Type)
		{
			case ModuleType.None:
				return Enumerable.Empty<Pulse>();
			case ModuleType.FlipFlop:
				return DoFlipFlop(pulse, module.Destinations);
			case ModuleType.Conjunction:
				return DoConjunction(pulse, module.Destinations);
		}

		throw new Exception("Something went wrong");
	}

	private IEnumerable<Pulse> DoFlipFlop(Pulse pulse, List<string> destinations)
	{
		if(pulse.Signal)
			return Enumerable.Empty<Pulse>();

		State.FlipFlops[pulse.Target] ^= true;
		var signal = State.FlipFlops[pulse.Target];

		return destinations.Select(d => new Pulse(signal, d, pulse.Target));
	}

	private IEnumerable<Pulse> DoConjunction(Pulse pulse, List<string> destinations)
	{
		var dict = State.Conjunctions[pulse.Target];
		dict[pulse.Source] = pulse.Signal;

		var signal = dict.Values.Any(p => p == false);

		return destinations.Select(d => new Pulse(signal, d, pulse.Target));
	}

	private void AddToCunt(bool pulseType)
	{
		if(pulseType)
			HighCount++;
		else
			LowCount++;
	}

	private readonly record struct Pulse(bool Signal, string Target, string Source);
}