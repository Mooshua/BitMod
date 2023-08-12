namespace BitMod.Commands.Handlers;

public class CommandResult
{
	private CommandResult(string? issue, bool success)
	{
		Issue = issue;
		Success = success;
	}

	public string? Issue { get; }

	public bool Success { get; }

	public static CommandResult Error(string issue)
		=> new CommandResult(issue, false);

	public static CommandResult Ok()
		=> new CommandResult(null, true);
}
