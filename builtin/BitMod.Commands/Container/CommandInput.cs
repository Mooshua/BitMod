using System.Text.RegularExpressions;

using BattleBitAPI.Server;

using BitMod.Commands.Sources;
using BitMod.Compatibility;
using BitMod.Events.Accessors;

using Lilikoi.Context;

namespace BitMod.Commands.Handlers;

public class CommandInput : Mount, IResponsiblePlayerAccessor, IRelevantGameserverAccessor
{
	public CommandInput(ICommandSource sender, string command, string[] arguments)
	{
		Sender = sender;
		Arguments = arguments;
		Command = command;
	}

	/// <summary>
	/// The source that sent this command
	/// </summary>
	public ICommandSource Sender { get; }

	public string[] Arguments { get; }

	public string Command { get; }

	/// <inheritdoc />
	public BitPlayer? ResponsiblePlayer => Sender.Player;

	/// <inheritdoc />
	public GameServer? RelevantGameserver => Sender.GameServer;

	public static CommandInput FromString(ICommandSource source, string command)
	{
		var args = Regex.Matches(command, @"[\""].+?[\""]|[^ ]+")
			.Cast<Match>()
			.Select(m => m.Value)
			.ToArray();

		var first = args.Take(1).First();
		var arguments = args.Skip(1).ToArray();

		return new CommandInput(source, first, arguments);
	}
}
