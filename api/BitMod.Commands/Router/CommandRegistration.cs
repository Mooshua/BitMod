namespace BitMod.Commands.Router;

public class CommandMetadata
{
	public CommandMetadata(string command)
	{
		Command = command;
	}

	public string Command { get; }
}
