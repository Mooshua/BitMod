namespace BitMod.Extensions.Commands.Lilikoi;

public class CommandDescription
{
	public CommandDescription(string name, string help, string[] alias)
	{
		Name = name;
		Help = help;
		Aliases = alias;
	}

	public string Name { get; }

	public string Help { get; }

	public string[] Aliases { get; }
}
