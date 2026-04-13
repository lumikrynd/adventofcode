namespace Y2022.Day07.Model.Commands;

public class CD : ICommand
{
	public string Target { get; }
	public CD(string target)
	{
		Target = target;
	}

	public override string ToString()
	{
		return $"cd {Target}";
	}
}